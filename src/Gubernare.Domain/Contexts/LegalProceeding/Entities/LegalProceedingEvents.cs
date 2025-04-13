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
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be empty.", nameof(type));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty.", nameof(status));

            LegalProceedingId = legalProceedingId;  // Define a FK

            Description = description;
            Date = date;
            Type = type;
            Status = status;
            LegalDeadline = legalDeadline;
        }

        #region Métodos de comportamento (exemplo)

        /// <summary>
        /// Exemplo de método de domínio para atualizar o status do evento.
        /// </summary>
        public void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                throw new ArgumentException("Status cannot be empty.", nameof(newStatus));

            Status = newStatus;
        }

        /// <summary>
        /// Exemplo de método para redefinir o prazo legal do evento.
        /// </summary>
        public void SetLegalDeadline(DateTime? newDeadline)
        {
            // Validações específicas podem ser aplicadas.
            LegalDeadline = newDeadline;
        }
        #endregion
    }
}
