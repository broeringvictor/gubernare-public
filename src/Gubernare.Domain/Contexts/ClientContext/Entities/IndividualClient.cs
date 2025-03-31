using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

namespace Gubernare.Domain.Contexts.ClientContext.Entities;

public class IndividualClient
{
    public string Name { get;  private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? ZipCode { get; private set; }
    public string? Street { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? JobTitle { get; private set; }
    public string? MaritalStatus { get; private set; }
    public string? BirthDate { get; private set; }
    
    public string? Notes { get; private set; }
    public Cpf CpfNumber { get; private set; } = null!;
    public Rg RgNumber { get; private set; } = null!;
    
}