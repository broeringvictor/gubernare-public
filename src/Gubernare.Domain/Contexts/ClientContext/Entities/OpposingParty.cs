using Gubernare.Domain.Contexts.SharedContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;
using System;
using Gubernare.Domain.Contexts.SharedContext.Enums;

namespace Gubernare.Domain.Contexts.ClientContext.Entities
{


    public class OpposingParty : Entity
    {
        /// <summary>
        /// Construtor protegido para uso do ORM (EF Core).
        /// </summary>
        protected OpposingParty()
        {
        }

        /// <summary>
        /// Construtor principal, que exige o tipo (Pessoa Física/Jurídica) e os demais campos.
        /// </summary>
        public OpposingParty(
            PersonType personType,
            string name,
            string? notes,
            string? phone,
            string? email,
            string? zipCode,
            string? street,
            string? city,
            string? state,
            string? country,
            string? jobTitle,
            string? maritalStatus,
            string? homeland,
            string? cnpj,
            string? inscricaoEstadual,
            string? inscricaoMunicipal,
            string? lawyers,
            Cpf? cpfNumber,
            Rg? rgNumber
        )
        {
            PersonType = personType;
            Name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentNullException(nameof(name), "Opposing party name cannot be empty.");

            Notes = notes;
            Phone = phone;
            Email = email;
            ZipCode = zipCode;
            Street = street;
            City = city;
            State = state;
            Country = country;
            Lawyers = lawyers;

            JobTitle = jobTitle;
            MaritalStatus = maritalStatus;
            Homeland = homeland;

            Cnpj = cnpj;
            InscricaoEstadual = inscricaoEstadual;
            InscricaoMunicipal = inscricaoMunicipal;

            CpfNumber = cpfNumber;
            RgNumber = rgNumber;

            ValidateByType();
        }

        /// <summary>
        /// Enum que indica se é Pessoa Física ou Jurídica.
        /// </summary>
        public PersonType PersonType { get; private set; }

        public List<LegalProceeding.Entities.LegalProceeding> LegalProceedings { get; set; } = new();
        
        public string Name { get; private set; }

        public string? Notes { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }

        // Endereço
        public string? ZipCode { get; private set; }
        public string? Street { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? Country { get; private set; }

        // Pessoa Física
        public string? JobTitle { get; private set; }
        public string? MaritalStatus { get; private set; }
        public string? Homeland { get; private set; }
        public Cpf? CpfNumber { get; private set; }
        public Rg? RgNumber { get; private set; }

        // Pessoa Jurídica
        public string? Cnpj { get; private set; }
        public string? InscricaoEstadual { get; private set; }
        public string? InscricaoMunicipal { get; private set; }

        // Ex.: Advogados associados ao “réu” se aplicável
        public string? Lawyers { get; private set; }

        #region Métodos Privados de Validação

        private void ValidateByType()
        {
            if (PersonType == SharedContext.Enums.PersonType.NaturalPerson)
            {
                // Se quiser obrigar CPF e RG quando PartyType=Person, faça:
                // if (CpfNumber is null)
                //     throw new DomainException("CPF is required for a physical person.");

                // if (RgNumber is null)
                //     throw new DomainException("RG is required for a physical person.");
            }
            else if (PersonType == PersonType.LegalEntity)
            {
                // Se quiser obrigar CNPJ quando PartyType=LegalEntity, faça:
                // if (string.IsNullOrWhiteSpace(Cnpj))
                //     throw new DomainException("CNPJ is required for a legal entity.");
            }
        }

        #endregion
    }
}
