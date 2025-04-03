using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

namespace Gubernare.Tests.Entities.Entities;

[TestClass]
public class ClientTests
{
    private static readonly Cpf _cpf = new Cpf("887.356.880-76");
    private static readonly Rg _rg = new Rg("41.308.783-9");
    private readonly IndividualClient _individualClient = new IndividualClient
    (
        name: "John Doe",
        email: "johndoe@example.com",
        phone: "+55 11 91234-5678",
        zipCode: "01234-567",
        street: "Rua das Flores, 123",
        city: "São Paulo",
        state: "SP",
        country: "Brasil",
        jobTitle: "Software Engineer",
        maritalStatus: "Single",
        homeland: "Brasil",
        cpfNumber: _cpf, // CPF com apenas números
        rgNumber: _rg, // RG com apenas números
        birthDate: new DateTime(1990, 5, 20),
        fristContact: "Whatapp"
    );


    [TestMethod]
    [TestCategory("Domain")]
    public void Ao_criar_um_usuario_deve_ser_possivel_obter_um_GUID()
    {
        Assert.IsInstanceOfType(_individualClient.Id, typeof(Guid));
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Tentar_criar_um_CPF_invalido_deve_retornar_erro()
    {
        var ex = Assert.ThrowsException<Exception>(() => new Cpf("123.456.789-00"));
        Assert.AreEqual("CPF inválido.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Criar_um_CPF_deve_ser_valido_e_retornar_11_caracteres()
    {
        Assert.AreEqual(11, _cpf.ToString().Length);
    }

}
