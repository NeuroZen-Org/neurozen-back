namespace neurozen.API.Appointments.Domain.Model.ValueObjects;

/// <summary>
/// Value Object que representa los tipos de citas disponibles
/// </summary>
public enum EAppointmentType
{
    /// <summary>
    /// Primera consulta con el profesional
    /// </summary>
    TerapiaIndividual = 1,
    
    /// <summary>
    /// Consulta de seguimiento
    /// </summary>
    ConsultaInicial = 2,
    
    /// <summary>
    /// Sesión de terapia regular
    /// </summary>
    Seguimiento = 3,
    
}

