using Gubernare.Domain.Contexts.SharedContext.Entities;
using Contract = Gubernare.Domain.Contexts.ClientContext.Entities.Contract;

namespace Gubernare.Domain.Contexts.AccountContext.Entities;

public class ContractOwner : Entity
{
    protected ContractOwner()
    {
    }

    #region Properties
    
    public User UserId { get; private set; }
    public int? PercentagePeferrals { get; private set; }
    public Contract ContractId { get; private set; }
    public DateTime? InitDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    
    #endregion

    #region Constructor

    public ContractOwner(
        User userId,
        int? percentagePeferrals,
        Contract contractId,
        DateTime? initDateTime,
        DateTime? endDateTime)
    {
        UserId = userId;
        PercentagePeferrals = percentagePeferrals;
        ContractId = contractId;
        InitDateTime = initDateTime;
        EndDateTime = endDateTime;
    }
    
    

    #endregion
    
    
}