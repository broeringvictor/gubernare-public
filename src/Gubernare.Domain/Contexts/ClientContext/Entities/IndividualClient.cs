using Gubernare.Domain.Contexts.SharedContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

namespace Gubernare.Domain.Contexts.ClientContext.Entities;

public class IndividualClient : Entity
{

    protected IndividualClient()
    {
    }

    #region properties
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
    public Cpf? CpfNumber { get; private set; }
    public Rg? RgNumber { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public DateTime FristContactAt { get; private set; } = DateTime.Now;
    public string? FristContact { get; private set; }
    public List<Contract> Contracts { get; private set; } = new();

    #endregion

    #region Constructor

    // Construtor completo (aceitando todos os campos)
    public IndividualClient(
        string name,
        string? email,
        string? phone,
        string? zipCode,
        string? street,
        string? city,
        string? state,
        string? country,
        string? jobTitle,
        string? maritalStatus,
        string? homeland,
        Cpf? cpfNumber,
        Rg? rgNumber,
        DateTime? birthDate,
        string? fristContact
        )
    {
        Name = name;
        Email = email;
        Phone = phone;
        ZipCode = zipCode;
        Street = street;
        City = city;
        State = state;
        Country = country;
        JobTitle = jobTitle;
        MaritalStatus = maritalStatus;
        Homeland = homeland;
        CpfNumber = cpfNumber;
        RgNumber = rgNumber;
        BirthDate = birthDate;
        FristContact = fristContact;

    }
    #endregion
}
