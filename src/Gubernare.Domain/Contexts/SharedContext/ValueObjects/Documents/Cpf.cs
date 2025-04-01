using DocumentValidator;

namespace Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents
{
    public class Cpf : ValueObject
    {
        public string CpfValue { get; }

        protected Cpf() { }

        public Cpf(string cpfValue)
        {
            try
            {
                // Remove qualquer caractere que não seja dígito
                var apenasDigitos = new string(cpfValue
                    .Where(char.IsDigit)
                    .ToArray());

                // Faz a validação no padrão "somente números"
                if (!CpfValidation.Validate(apenasDigitos))
                {
                    throw new Exception("CPF inválido.");
                }

                // Armazena somente os dígitos
                CpfValue = apenasDigitos;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao validar o CPF: {e.Message}");
                throw;
            }
        }

        public override string ToString()
        {
            // Retorna sempre o CPF sem pontuação (ex: "11111111111")
            return CpfValue;
        }
    }
}
