﻿using neurozen.API.Appointments.Domain.Model.Commands;
using neurozen.API.Appointments.Interfaces.REST.Resources;

namespace neurozen.API.Appointments.Interfaces.REST.Transform;

public class CreateAppointmentCommandFromResourceAssembler
{
    public static CreateAppointmentCommand ToCommandFromResource(CreateAppointmentResource resource) =>
        new CreateAppointmentCommand(
            resource.PatientId, 
            resource.ProfessionalId, 
            resource.AppointmentDateTime,
            resource.AppointmentType,
            resource.NotasAdicionales);
}