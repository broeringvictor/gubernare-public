using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.Entities;

public class LegalProceeding : Entity
{
    protected LegalProceeding(){}
    
    public string Name { get; private set; } = string.Empty;
    List<IndividualClient> IndividualClients { get; set; } = new();
    public string ClientRole { get; private set; } = string.Empty;
    List<Contract> Contracts { get; set; } = new();
    List<LegalProceedingEvents> LegalProceedingEvents { get; set; } = new();
    List<OpposingParty> OpposingParties { get; set; } = new();
    public string OpposingPartyRole { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string LegalCourt { get; private set; } = string.Empty;
    public string AccessCode { get; private set; } = string.Empty;
    public DateTime Date { get; private set;}
    public string Type { get; private set;} = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime? FinishedDateTime { get; private set; }


    #region Constructor

// Construtor completo (aceitando todos os campos)
    public LegalProceeding(
        string name,
        List<IndividualClient> individualClients,
        string clientRole,
        string description,
        string legalCourt,
        string accessCode,
        DateTime date,
        string type,
        string status,
        DateTime? finishedDateTime
    )
    {
        Name = name;
        ClientRole = clientRole;
        Description = description;
        LegalCourt = legalCourt;
        AccessCode = accessCode;
        Date = date;
        Type = type;
        Status = status;
        FinishedDateTime = finishedDateTime;
    }

    #endregion
    
    
    
    
}
