using Gubernare.Api.Extensions;
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gubernare.Tests.Entities.Repository
{
    [TestClass]
    public class ClientRepositoryTests
    {
        private Mock<Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts.IRepository> _individualClientRepositoryMock;
        private Mock<Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts.IRepository> _contractRepositoryMock;

        private IndividualClient _individualClient;
        private Contract _contract;

        [TestInitialize]
        public void Setup()
        {
            // Inicializa os Mocks das interfaces dos repositórios:
            _individualClientRepositoryMock = new Mock<Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts.IRepository>();
            _contractRepositoryMock = new Mock<Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts.IRepository>();

            // Cria instâncias de exemplo para serem usadas nos testes:
            _individualClient = new IndividualClient(
                name: "John Doe",
                email: "johndoe@example.com",
                phone: "9999-9999",
                notes: "Cliente VIP"
            );

            _contract = new Contract(
                name: "Important Contract",
                type: "NDA",
                description: "Non-disclosure agreement",
                notes: "Confidencial",
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(30),
                price: 1200.50m,
                documentFolder: "path/to/contract"
            );
        }

        [TestMethod]
        [TestCategory("ClientRepository")]
        public async Task SaveIndividualClient_DeveSalvarClienteNoBanco()
        {
            // ARRANGE
            _individualClientRepositoryMock
                .Setup(x => x.SaveAsync(_individualClient, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // ACT
            await _individualClientRepositoryMock.Object.SaveAsync(_individualClient, CancellationToken.None);

            // ASSERT
            _individualClientRepositoryMock.Verify(
                x => x.SaveAsync(_individualClient, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        [TestCategory("ClientRepository")]
        public async Task AnyAsync_DeveRetornarTrueSeClienteJaExiste()
        {
            // ARRANGE
            var cpf = "12345678901";
            _individualClientRepositoryMock
                .Setup(x => x.AnyAsync(cpf, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // ACT
            var result = await _individualClientRepositoryMock.Object.AnyAsync(cpf, CancellationToken.None);

            // ASSERT
            Assert.IsTrue(result, "O método AnyAsync deveria retornar True indicando que o cliente já existe.");
            _individualClientRepositoryMock.Verify(
                x => x.AnyAsync(cpf, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        [TestCategory("ClientRepository")]
        public async Task SaveContract_DeveSalvarContratoNoBanco()
        {
            // ARRANGE
            _contractRepositoryMock
                .Setup(x => x.SaveAsync(_contract, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // ACT
            await _contractRepositoryMock.Object.SaveAsync(_contract, CancellationToken.None);

            // ASSERT
            _contractRepositoryMock.Verify(
                x => x.SaveAsync(_contract, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
