using FluentValidation;
using Template.Application.HttpServices;
using Template.Application.Services;
using Template.Domain.Entities;

namespace MicroservicioReservas.Validation
{
    public class ReservaValidator : AbstractValidator<ReservaDTO>
    {
        public ReservaValidator(MicroservicioHotelService hotelService, IEstadoReservaService estadoReservaService)
        {
            RuleFor(r => r.HotelId)
                .MustAsync(async (x, cancellable) =>
                {
                    return await hotelService.CheckIfHotelExists(x);
                }).WithMessage("La ID del hotel ingresado no es válida");

            RuleFor(r => r.HabitacionId)
                .MustAsync(async (model, x, cancellable) =>
                {
                    return await hotelService.CheckIfHabitacionExists(model.HotelId, x);
                }).WithMessage("La ID de la habitación ingresada no es válida");

            RuleFor(r => r.EstadoReservaId)
                .MustAsync(async (x, cancellable) =>
                {
                    return await estadoReservaService.CheckIfEstadoExists(x);
                }).WithMessage("No se ha ingresado una ID de estado de reserva válida");
        }
    }
}
