using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.ClientContext.Entities;

public class Contract : Entity
{
    private Contract() { }

    #region Proprieties
    
    public string Name { get; private set; } = String.Empty;
    public string? Type { get; private set; }
    public string Description { get; private set; } = String.Empty;
    public string? Notes { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal? Price { get; private set; }
    public string? DocumentFolder { get; private set; }
    
    public List<IndividualClient> IndividualClient { get;private set; } = new List<IndividualClient>();

  
    #endregion
}