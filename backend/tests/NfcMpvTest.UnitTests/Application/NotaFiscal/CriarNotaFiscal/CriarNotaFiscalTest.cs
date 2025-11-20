using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NfcMpvTest.Application.UseCases.CriarNotaFiscal;
using DomainEntity = NfcMpvTest.Domain.Entity;
using UseCase = NfcMpvTest.Application.UseCases.CriarNotaFiscal;

namespace NfcMpvTest.UnitTests.Application.NotaFiscal.CriarNotaFiscal
{
    public class CriarNotaFiscalTest
    {
        private readonly CriarNotaFiscalUseCaseTestFixture _fixture;

        public CriarNotaFiscalTest() => _fixture = new CriarNotaFiscalUseCaseTestFixture();

        [Fact(DisplayName = nameof(Deve_Criar_NotaFiscal_Corretamente))]
        [Trait("Application", "CriarNotaFiscal - Use Case")]
        public async Task Deve_Criar_NotaFiscal_Corretamente()
        {
            var exemploNotafiscal = _fixture.RetornaNotaFiscalValida(3);

            var unitOfWorkMock = new Mock<NfcMpvTest.Domain.Repository.IUnitOfWork>();

            var notaFiscalService = new Mock<NfcMpvTest.Domain.Services.INotaFiscalService>();

            notaFiscalService
                .Setup(x => x.RegisterAsync(It.IsAny<DomainEntity.NotaFiscal>()))
                .ReturnsAsync(exemploNotafiscal);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var useCase = new UseCase.CriarNotaFiscalUseCase(
                notaFiscalService.Object,
                unitOfWorkMock.Object,
                serviceProvider.GetRequiredService<ILogger<CriarNotaFiscalUseCase>>());

            var notaFiscaValida = _fixture.RetornaNotaFiscalRequestValida();

            var emissor = exemploNotafiscal.Emissor;
            var dataEmissao = exemploNotafiscal.DataEmissao;
            var itens = _fixture.RetornaItemRequestList(3);

            var input = new CriarNotaFiscalRequest(
                emissor,
                dataEmissao,
                itens
            );

            var output = await useCase.Handle(input, CancellationToken.None);

            notaFiscalService.Verify(service =>
            service.RegisterAsync(
                It.IsAny<DomainEntity.NotaFiscal>()),
                Times.Once);

            unitOfWorkMock.Verify(uow => uow.CommitAsync(
                It.IsAny<CancellationToken>()),
                Times.Once);

            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Emissor.Should().Be(emissor);
            output.DataEmissao.Should().Be(dataEmissao);
            output.Status.Should().Be(NfcMpvTest.Domain.Enum.NotaFiscalStatus.Emitida);
            output.Items.Should().HaveCount(itens.Count);
        }
    }
}
