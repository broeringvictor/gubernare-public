using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.Entities;

public class LegalProceedingEvents : Entity
{
    protected LegalProceedingEvents(){}
    
    LegalProceeding LegalProceedingId { get; set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime Date { get; private set;}
    public string Type { get; private set;} = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime? LegalDeadline { get; private set; }


    
}