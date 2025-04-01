using DocumentValidator;

namespace Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents
{
    public class Rg : ValueObject
    {
        public string RgValue { get; }

        protected Rg() { }

        public Rg(string rgValue)
        {
            try
            {

                var apenasDigitos = new string(rgValue
                    .Where(char.IsDigit)
                    .ToArray());


                if (!RGValidation.Validate(apenasDigitos))
                {
                    throw new Exception("RG inválido.");
                }

                // Armazena somente os dígitos
                RgValue = apenasDigitos;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao validar o RG: {e.Message}");
                throw;
            }
        }

        public override string ToString() => RgValue;
    }
}
