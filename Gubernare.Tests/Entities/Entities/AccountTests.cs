using System.Reflection.Metadata.Ecma335;
using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.ValueObjects;

namespace Gubernare.Tests.Entities.Entities;

[TestClass]
public class AccountTests
{
    private readonly User _user = new User("victor@victorbroering.adv.br", "<PAssWORD!@1011>");
    private readonly Password _password = new Password("<PAssWORD!@1011>");
    private readonly Verification _verification = new Verification();

    [TestMethod]
    [TestCategory("Domain")]
    public void Ao_criar_um_usuario_deve_ser_possivel_obter_um_GUID()
    {
        Assert.IsInstanceOfType(_user.Id, typeof(Guid));
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Tentar_criar_um_usuario_com_email_invalido_deve_retornar_erro()
    {
        Assert.ThrowsException<Exception>(() => new User("victor", "SenhaSegura!@"));
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Tentar_criar_um_password_deve_retornar_Hash_8_caracteres()
    {
        Assert.AreEqual(8, _password.GetHashCode().ToString().Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Tentar_descriptografar_um_password()
    {
        Assert.IsTrue(_user.Password.Challenge("<PAssWORD!@1011>"));

        Assert.IsFalse(_user.Password.Challenge("senhaErrada"));
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Código_de_verificação_de_senha_é_valido_e_retorna_true()
    {
        Assert.IsTrue(_user.Password.Challenge("<PAssWORD!@1011>"));

        Assert.IsFalse(_user.Password.Challenge("senhaErrada"));
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Validação_do_Código_enviado_por_email_para_ativar_a_conta()
    {
        var verification = new Verification();

        verification.Verify(verification.Code);

        // Depois tenta verificar novamente — deve lançar exceção
        Assert.ThrowsException<Exception>(() =>
        {
            verification.Verify(verification.Code);
        });
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Deve_lancar_excecao_quando_verificacao_ja_foi_feita()
    {
        // Arrange
        var verification = new Verification();
        verification.Verify(verification.Code); // Primeira verificação bem-sucedida

        // Act & Assert
        var ex = Assert.ThrowsException<Exception>(() => verification.Verify(verification.Code));
        Assert.AreEqual("Este item já foi ativado", ex.Message);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Deve_lancar_excecao_quando_codigo_esta_expirado()
    {
        // Arrange
        var verification = new Verification();

        // Forçar expiração
        typeof(Verification)
            .GetProperty("ExpiresAt")
            ?.SetValue(verification, DateTime.UtcNow.AddMinutes(-1));

        // Act & Assert
        var ex = Assert.ThrowsException<Exception>(() => verification.Verify(verification.Code));
        Assert.AreEqual("Este código já expirou", ex.Message);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Deve_lancar_excecao_quando_codigo_invalido()
    {
        // Arrange
        var verification = new Verification();
        var codigoIncorreto = "ABC123"; // deliberadamente diferente do gerado

        // Act & Assert
        var ex = Assert.ThrowsException<Exception>(() => verification.Verify(codigoIncorreto));
        Assert.AreEqual("Código de verificação inválido", ex.Message);
    }



}
