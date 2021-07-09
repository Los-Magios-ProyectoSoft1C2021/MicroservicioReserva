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
                    var habitaciones = await hotelService.GetHabitacionesByHotelAndTipo(model.HotelId, x);

                    if (habitaciones != null)
                        return habitaciones.Count > 0;
                    else
                        return false;
                }).WithMessage("No hay habitaciones disponibles dentro de esa categoría");

            RuleFor(r => r.FechaFin)
                .Must((model, x, y) => x > model.FechaInicio).WithMessage("La fecha de fin no puede ser menor a la fecha inicial");
        }
    }
}
