namespace neurozen.API.Appointments.Domain.Model.ValueObjects;

/// <summary>
/// Value Object que representa el tipo de cita con su lógica de negocio
/// </summary>
public record AppointmentType
{
    public EAppointmentType Type { get; init; }
    public string DisplayName { get; init; }
    public string Description { get; init; }
    public int EstimatedDurationMinutes { get; init; }

    private AppointmentType(EAppointmentType type, string displayName, string description, int durationMinutes)
    {
        Type = type;
        DisplayName = displayName;
        Description = description;
        EstimatedDurationMinutes = durationMinutes;
    }

    // Valores predefinidos (Value Object Pattern)
    public static readonly AppointmentType TerapiaIndividual = new(
        EAppointmentType.TerapiaIndividual, 
        "Terapia Individual", 
        "Sesión personalizada uno a uno con el profesional",
        60);

    public static readonly AppointmentType ConsultaInicial = new(
        EAppointmentType.ConsultaInicial, 
        "Consulta Inicial", 
        "Primera consulta para evaluación y diagnóstico",
        45);

    public static readonly AppointmentType Seguimiento = new(
        EAppointmentType.Seguimiento, 
        "Seguimiento", 
        "Sesión de seguimiento y revisión de progreso",
        30);

    /// <summary>
    /// Crea un AppointmentType a partir del enum
    /// </summary>
    public static AppointmentType FromType(EAppointmentType type)
    {
        return type switch
        {
            EAppointmentType.TerapiaIndividual => TerapiaIndividual,
            EAppointmentType.ConsultaInicial => ConsultaInicial,
            EAppointmentType.Seguimiento => Seguimiento,
            _ => throw new ArgumentException($"Tipo de cita no válido: {type}")
        };
    }

    /// <summary>
    /// Obtiene todos los tipos de cita disponibles
    /// </summary>
    public static IEnumerable<AppointmentType> GetAll()
    {
        yield return TerapiaIndividual;
        yield return ConsultaInicial;
        yield return Seguimiento;
    }

    /// <summary>
    /// Valida si el tipo de cita es válido
    /// </summary>
    public static bool IsValid(EAppointmentType type)
    {
        return Enum.IsDefined(typeof(EAppointmentType), type);
    }
}

