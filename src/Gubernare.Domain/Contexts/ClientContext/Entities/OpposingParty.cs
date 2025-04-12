using Gubernare.Domain.Contexts.SharedContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

namespace Gubernare.Domain.Contexts.ClientContext.Entities;

public class OpposingParty : Entity
{
    protected OpposingParty(){}
    
    public string Name { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? ZipCode { get; private set; }
    public string? Street { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? JobTitle { get; private set; }
    public string? MaritalStatus { get; private set; }
    public string? Homeland { get; private set; }
    public string? Cnpj { get; private set; }
    public string? InscricaoEstadual { get; private set; }
    public string? InscricaoMunicipal { get; private set; }
    public string? Lawyers { get; private set; }
    public Cpf? CpfNumber { get; private set; }
    public Rg? RgNumber { get; private set; }
    
}