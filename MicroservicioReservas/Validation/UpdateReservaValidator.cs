﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.HttpServices;
using Template.Application.Services;
using Template.Domain.DTOs.Request;

namespace MicroservicioReservas.Validation
{
    public class UpdateReservaValidator : AbstractValidator<RequestUpdateReservaDTO>
    {
        public UpdateReservaValidator(IEstadoReservaService estadoReservaService)
        {
            RuleFor(r => r.ReservaId)
                .NotEmpty().WithMessage("La ID de la reserva no puede estar vacía");

            RuleFor(r => r.EstadoReservaId)
                .MustAsync(async (x, cancellable) =>
                {
                    return await estadoReservaService.CheckIfEstadoExists(x);
                }).WithMessage("No se ha ingresado una ID de estado de reserva válida");
        }
    }
}