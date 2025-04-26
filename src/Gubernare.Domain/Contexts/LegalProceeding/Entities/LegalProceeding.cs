using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.Enums;
using Gubernare.Domain.Contexts.SharedContext.Entities;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Gubernare.Domain.Contexts.LegalProceeding.Entities
{
    public class LegalProceeding : Entity
    {

        protected LegalProceeding()
        {

        }
        List<OpposingParty> opposingParties = new List<OpposingParty>();
        List<IndividualClient> individualClients = new List<IndividualClient>();
        List<LegalProceedingEvent> legalProceedingEvents = new List<LegalProceedingEvent>();
        
        [JsonPropertyName("Number")]
        public string Number { get; private set; }
        public string? Name { get; private set; }
        public string? ClientRole { get; private set; }
        public string? OpposingPartyRole { get; private set; }
        public CourtInstances? CourtInstance { get; private set; }
        
              
        [JsonPropertyName("CourtDivisionName")]
        public string? CourtDivisionName { get; private set; }
        public string LegalCourt { get; private set; }
        
        [JsonPropertyName("CauseValue")]
        public double? CauseValue { get; private set; }

        public string? Description { get; private set; }
  
        public string AccessCode { get; private set; }
        public DateTime? DistributionDate { get; private set; }
        
        [JsonPropertyName("Type")]
        public string Type { get; private set; }
        public string Status { get; private set; }
        public DateTime? FinishedDateTime { get; private set; }

        
        private readonly List<IndividualClient> _individualClients;
        public IReadOnlyCollection<IndividualClient> IndividualClients => _individualClients.AsReadOnly();

        private readonly List<Contract> _contracts;
        public List<ToDo?> Tasks { get; set; } = new();
   

        private readonly List<LegalProceedingEvent> _legalProceedingEvents;
        public IReadOnlyCollection<LegalProceedingEvent> LegalProceedingEvents => _legalProceedingEvents.AsReadOnly();

        private readonly List<OpposingParty> _opposingParties;
        public IReadOnlyCollection<OpposingParty> OpposingParties => _opposingParties.AsReadOnly();

        // Construtor principal
        public LegalProceeding(
            string number,
            string name,
            string clientRole,
            string? courtDivisionName,
            string? description,
            string legalCourt,
            string accessCode,
            DateTime? distributionDate,
            string type,
            string status,
            DateTime? finishedDateTime,
            CourtInstances? courtInstance = null,
            string? opposingPartyRole = null,
            List<IndividualClient>? individualClients = null,
            List<LegalProceedingEvent>? legalProceedingEvents = null,
            List<OpposingParty>? opposingParties = null
        )
        {

            Number = number;
            Name = name ?? "Não informado";
            ClientRole = clientRole;
            Description = description ?? string.Empty;
            CourtDivisionName = courtDivisionName ?? string.Empty;
            LegalCourt = legalCourt ?? string.Empty;
            AccessCode = accessCode ?? string.Empty;
            DistributionDate = distributionDate;
            Type = type ?? string.Empty;
            Status = status ?? string.Empty;
            FinishedDateTime = finishedDateTime;
            CourtInstance = courtInstance;
            OpposingPartyRole = opposingPartyRole;


            _individualClients = individualClients ?? new List<IndividualClient>();

            _legalProceedingEvents = legalProceedingEvents ?? new List<LegalProceedingEvent>();
            _opposingParties = opposingParties ?? new List<OpposingParty>();
        }

        #region Métodos de comportamento (exemplos)

        public void AddIndividualClient(IndividualClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _individualClients.Add(client);
        }

        public void CloseProceeding(DateTime finishedDate)
        {
            Status = "Encerrado";
            FinishedDateTime = finishedDate;
        }

        #endregion
    }
}
