namespace Gubernare.Domain.Contexts.ClientContext.Entities;

public class Contract
{
    public string Case { get; private set; }
    public string? Type { get; private set; }
    public IndividualClient? IndividualClient { get;private set; }
    public LegalEntityClient? LegalEntityClient { get; private set; }
    public string Description { get; private set; }
    public string? Notes { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal Price { get; private set; }
    public string? DocumentFolder { get; private set; }
    
}