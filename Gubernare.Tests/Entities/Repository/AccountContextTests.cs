using Gubernare.Api.Extensions;
using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.ValueObjects;
using Moq;

namespace Gubernare.Tests.Entities.Repository;

[TestClass]
public class RepositoryTests
{
    private static Password _password = new Password("<PASSWORD>!");
    private User _user = new User("John Doe","victorbroeringsilva@gmail.com", _password);
    private Mock<Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository> _createRepositoryMock;
    private Mock<Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService> _serviceEmailMock;

    // Autenticação
    private Mock<Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository> _authRepositoryMock;




    [TestInitialize]
    public void Setup()
    {
        // Aqui inicializamos as instâncias dos mocks
        _createRepositoryMock = new Mock<Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository>();
        _serviceEmailMock = new Mock<Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService>();

        _authRepositoryMock = new Mock<Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository>();
    }

    [TestMethod]
    [TestCategory("Repository")]
    public async Task SaveAsync_DeveSalvarUsuarioNoBanco()
    {
        // ARRANGE
        // Configura o mock do repositório
        _createRepositoryMock
            .Setup(x => x.SaveAsync(_user, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Configura o mock do serviço que envia e-mail
        _serviceEmailMock
            .Setup(x => x.SendVerificationEmailAsync(_user, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // ACT
        await _createRepositoryMock.Object.SaveAsync(_user, CancellationToken.None);
        await _serviceEmailMock.Object.SendVerificationEmailAsync(_user, CancellationToken.None);

        // ASSERT
        _createRepositoryMock.Verify(
            x => x.SaveAsync(_user, It.IsAny<CancellationToken>()),
            Times.Once
        );

        _serviceEmailMock.Verify(
            x => x.SendVerificationEmailAsync(_user, It.IsAny<CancellationToken>()),
            Times.Once


        );

    }

    [TestMethod]
    [TestCategory("Repository")]
    public async Task Tentar_se_autenticar()
    {

        var expectedUser = _user;
        _authRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(expectedUser.Email.Address, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUser); // Retorna o usuário esperado como sendo resultado da chamada


        var result = await _authRepositoryMock.Object.GetUserByEmailAsync(expectedUser.Email.Address, CancellationToken.None);


        Assert.IsNotNull(result, "O método não retornou nenhum usuário.");

        Assert.AreEqual(expectedUser.Email.Address, result.Email.Address, "O e-mail do usuário retornado está incorreto.");
    }

}




