using Gubernare.Domain.Contexts.SharedContext.Entities;
using System;

namespace Gubernare.Domain.Contexts.LegalProceeding.Entities
{
    public class LegalProceedingEvent : Entity
    {
        // Construtor protegido para uso do ORM
        protected LegalProceedingEvent() { }

        // FK para LegalProceeding
        public LegalProceeding? LegalProceeding { get; private set; }
        public Guid LegalProceedingId { get; private set; } 

        public string Description { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }
        public string Type { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;
        public DateTime? LegalDeadline { get; private set; }

        /// <summary>
        /// Construtor principal, com validações básicas.
        /// </summary>
        public LegalProceedingEvent(
            Guid legalProceedingId,
            string description,
            DateTime date,
            string type,
            string status,
            DateTime? legalDeadline = null
        )
        {
            LegalProceedingId = legalProceedingId;  

            Description = description;
            Date = date;
            Type = type;
            Status = status;
            LegalDeadline = legalDeadline;
        }

        #region Métodos de comportamento (exemplo)


        public void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                throw new ArgumentException("Status cannot be empty.", nameof(newStatus));

            Status = newStatus;
        }

        public void SetLegalDeadline(DateTime? newDeadline)
        {
            // Validações específicas podem ser aplicadas.
            LegalDeadline = newDeadline;
        }
        #endregion
    }
}
