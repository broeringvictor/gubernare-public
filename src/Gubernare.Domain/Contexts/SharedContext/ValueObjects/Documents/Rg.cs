using DocumentValidator;

namespace Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

public class Rg : ValueObject
{
  
    public string RgValue { get; }

  
    protected Rg()
    {
    }


    public Rg(string rgValue)
    {
        try
        {
         
            if (!CpfValidation.Validate(rgValue))
            {
                throw new Exception("Rg inválido.");
            }

          
            RgValue = rgValue;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao validar o Rg: {e.Message}");
            throw;
        }
    }

    public override string ToString() => RgValue;
}