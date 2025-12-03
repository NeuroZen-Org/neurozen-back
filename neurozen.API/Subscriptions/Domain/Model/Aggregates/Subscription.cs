using neurozen.API.Subscriptions.Domain.Model.Commands;

namespace neurozen.API.Subscriptions.Domain.Model.Aggregates;

public partial class Subscription
{
    protected Subscription()
    {
        UserId = 0; // 👈 nuevo campo inicializado
        PlanId = 0;
        NameUser = string.Empty;
        LastNameUser = string.Empty;
        EmailUser = string.Empty;
        NumberCard = string.Empty;
        ExpirationDate = string.Empty;
        Cvv = string.Empty;
        IsActive = false; // 👈 Valor por defecto false
    }

    public Subscription(CreateSubscriptionCommand command)
    {
        UserId = command.UserId; // 👈 agregado
        PlanId = command.PlanId;
        NameUser = command.NameUser;
        LastNameUser = command.LastNameUser;
        EmailUser = command.EmailUser;
        NumberCard = command.NumberCard;
        ExpirationDate = command.ExpirationDate;
        Cvv = command.Cvv;
        IsActive = command.IsActive; 
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }   // 👈 nuevo campo
    public int PlanId { get; private set; }
    public string NameUser { get; private set; }
    public string LastNameUser { get; private set; }
    public string EmailUser { get; private set; }
    public string NumberCard { get; private set; }
    public string ExpirationDate { get; private set; }
    public string Cvv { get; private set; }
    
    public bool? IsActive { get; set; }
    
    /// <summary>
    /// Activa la suscripción después de ser guardada exitosamente
    /// </summary>
    public void Activate()
    {
        IsActive = true;
    }
    
    /// <summary>
    /// Desactiva la suscripción
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}