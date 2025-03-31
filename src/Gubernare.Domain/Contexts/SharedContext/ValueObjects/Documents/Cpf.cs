using DocumentValidator;

namespace Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

public class Cpf : ValueObject
{
  
    public string CpfValue { get; }

  
    protected Cpf()
    {
    }


    public Cpf(string cpfValue)
    {
        try
        {
         
            if (!CpfValidation.Validate(cpfValue))
            {
                throw new Exception("CPF inválido.");
            }

          
            CpfValue = cpfValue;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao validar o CPF: {e.Message}");
            throw;
        }
    }

    public override string ToString() => CpfValue;
}