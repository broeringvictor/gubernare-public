using System.Diagnostics.Contracts;

namespace Gubernare.Domain.Contexts.AccountContext.Entities;

public class ContractOwner
{
    public User Name { get; set; }
    public int? PercentagePeferrals { get; set; }
    public Contract ContractName { get; set; }
    

    
    
}