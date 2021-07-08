using FluentValidation;
using System;
using Template.Application.HttpServices;
using Template.Application.Services;
using Template.Domain.DTOs.Request;

namespace MicroservicioReservas.Validation
{
    public class CreateReservaValidator : AbstractValidator<RequestCreateReservaDTO>
    {
        public CreateReservaValidator(MicroservicioHotelService hotelService, IEstadoReservaService estadoReservaService)
        {
            RuleFor(r => r.HotelId)
                .MustAsync(async (x, cancellable) =>
                {
                    return await hotelService.CheckIfHotelExists(x);
                }).WithMessage("La ID del hotel ingresado no es válida");

            RuleFor(r => r.TipoHabitacionId)
                .MustAsync(async (model, x, cancellable) =>
                {
                    return await hotelService.CheckIfHabitacionExists(model.HotelId, x);
                }).WithMessage("La ID de la habitación ingresada no es válida");

            RuleFor(r => r.FechaInicio)
                .Must(x => x >= DateTime.Now).WithMessage("La fecha de inicio no puede ser menor a hoy");

            RuleFor(r => r.FechaFin)
                .Must((model, x, y) => x < model.FechaInicio).WithMessage("La fecha de fin no puede ser menor a la fecha final");

            /*
            RuleFor(r => r.EstadoReservaId)
                .MustAsync(async (x, cancellable) =>
                {
                    return await estadoReservaService.CheckIfEstadoExists(x);
                }).WithMessage("No se ha ingresado una ID de estado de reserva válida");
            */
        }
    }
}
