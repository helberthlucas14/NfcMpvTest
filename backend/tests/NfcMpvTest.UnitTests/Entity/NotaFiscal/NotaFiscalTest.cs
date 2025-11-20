using FluentAssertions;
using NfcMpvTest.Domain.Enum;
using NfcMpvTest.Domain.Exceptions;
using Entity = NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.UnitTests.Entity.NotaFiscal
{
    [Collection(nameof(NotaFiscalTestFixture))]
    public class NotaFiscalTest
    {

        private readonly NotaFiscalTestFixture _fixture;

        public NotaFiscalTest(NotaFiscalTestFixture fixture) => _fixture = fixture;

        [Fact(DisplayName = nameof(Deve_Criar_Nota_Fiscal_Corretamente))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Criar_Nota_Fiscal_Corretamente()
        {
            var emissor = _fixture.RetornaEmissorValido();
            var dataEmissao = _fixture.RetornaDataEmissaoValida();

            var notaFiscal = new Domain.Entity.NotaFiscal(emissor, dataEmissao);

            notaFiscal.Emissor.Should().Be(emissor);
            notaFiscal.DataEmissao.Should().Be(dataEmissao);
            notaFiscal.Items.Should().BeEmpty();
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }

        [Fact(DisplayName = nameof(Deve_Criar_Nota_Fiscal_ComItens_Corretamente))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Criar_Nota_Fiscal_ComItens_Corretamente()
        {
            var emissor = _fixture.RetornaEmissorValido();
            var dataEmissao = _fixture.RetornaDataEmissaoValida();

            var notaFiscal = new Domain.Entity.NotaFiscal(emissor, dataEmissao);

            var itens = _fixture.RetornaListaItensValida(notaFiscal.Id, 10);

            foreach (var item in itens)
                notaFiscal.AdicionarItem(item);

            notaFiscal.Emissor.Should().Be(emissor);
            notaFiscal.DataEmissao.Should().Be(dataEmissao);
            notaFiscal.Items.Should().NotBeEmpty();
            notaFiscal.ValoTotal.Should().NotBe(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }


        [Fact(DisplayName = nameof(Deve_Calcular_Valor_Total_Corretamente))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Calcular_Valor_Total_Corretamente()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
                 _fixture.RetornaEmissorValido(),
                _fixture.RetornaDataEmissaoValida()
            );

            var item1 = new Domain.Entity.Item(
                notaFiscal.Id,
                _fixture.Faker.Commerce.ProductName(),
                100.50m,
                Guid.NewGuid()
            );

            var item2 = new Domain.Entity.Item(
                notaFiscal.Id,
                _fixture.Faker.Commerce.ProductName(),
                200.75m,
                Guid.NewGuid()
            );

            notaFiscal.AdicionarItem(item1);
            notaFiscal.AdicionarItem(item2);

            var valorTotal = notaFiscal.CalcularValorTotal();
            notaFiscal.ValoTotal.Should().Be(301.25m);
            notaFiscal.Items.Should().HaveCount(2);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_Emissor_Corretamente))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Atualizar_Emissor_Corretamente()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
            _fixture.RetornaEmissorValido(),
            _fixture.RetornaDataEmissaoValida());

            var novoEmissor = _fixture.RetornaEmissorValido();

            notaFiscal.Atualizar(novoEmissor);

            notaFiscal.Emissor.Should().Be(novoEmissor);
            notaFiscal.DataEmissao.Should().Be(notaFiscal.DataEmissao);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }


        [Fact(DisplayName = nameof(Deve_Atualizar_DataEmissao_Corretamente_Com_StatusEmitida))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Atualizar_DataEmissao_Corretamente_Com_StatusEmitida()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
            _fixture.RetornaEmissorValido(),
            _fixture.RetornaDataEmissaoValida());

            var novaDataEmissao = _fixture.RetornaDataEmissaoValida();

            notaFiscal.Atualizar(dataEmissao: novaDataEmissao);

            notaFiscal.Emissor.Should().Be(notaFiscal.Emissor);
            notaFiscal.DataEmissao.Should().Be(novaDataEmissao);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_DataEmissao_Corretamente_Com_EmProcessamento))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Atualizar_DataEmissao_Corretamente_Com_EmProcessamento()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
            _fixture.RetornaEmissorValido(),
            _fixture.RetornaDataEmissaoValida());

            var novaDataEmissao = _fixture.RetornaDataEmissaoValida();
            notaFiscal.ColocarEmProcessamento();

            notaFiscal.Atualizar(dataEmissao: novaDataEmissao);

            notaFiscal.Emissor.Should().Be(notaFiscal.Emissor);
            notaFiscal.DataEmissao.Should().Be(novaDataEmissao);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.EmProcessamento);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_DataEmissao_Corretamente_Com_StatusErro))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Atualizar_DataEmissao_Corretamente_Com_StatusErro()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
            _fixture.RetornaEmissorValido(),
            _fixture.RetornaDataEmissaoValida());

            var novaDataEmissao = _fixture.RetornaDataEmissaoValida();
            notaFiscal.DefinirErro();

            notaFiscal.Atualizar(dataEmissao: novaDataEmissao);

            notaFiscal.Emissor.Should().Be(notaFiscal.Emissor);
            notaFiscal.DataEmissao.Should().Be(novaDataEmissao);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Erro);
        }


        [Fact(DisplayName = nameof(Deve_Remover_Item_Corretamente_Com_Status_EmProcessamento))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Remover_Item_Corretamente_Com_Status_EmProcessamento()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
               _fixture.RetornaDataEmissaoValida()
           );

            var item1 = new Domain.Entity.Item(
                notaFiscal.Id,
                _fixture.Faker.Commerce.ProductName(),
                400.20m,
                Guid.NewGuid()
            );

            var item2 = new Domain.Entity.Item(
                notaFiscal.Id,
                _fixture.Faker.Commerce.ProductName(),
                145.02m,
                Guid.NewGuid()
            );

            notaFiscal.AdicionarItem(item1);
            notaFiscal.AdicionarItem(item2);
            notaFiscal.RemoverItem(item1);

            notaFiscal.ColocarEmProcessamento();
            notaFiscal.Items.Should().HaveCount(1);

            notaFiscal.ValoTotal.Should().Be(145.02m);

            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.EmProcessamento);
        }


        [Fact(DisplayName = nameof(Deve_Remover_TodosItens_Corretamente_Status_Emitida))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Remover_TodosItens_Corretamente_Status_Emitida()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
               _fixture.RetornaDataEmissaoValida()
           );

            var itens = _fixture.RetornaListaItensValida(notaFiscal.Id, 5);
            foreach (var item in itens)
                notaFiscal.AdicionarItem(item);

            notaFiscal.RemoverTodosItens();
            notaFiscal.Items.Should().HaveCount(0);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Emitida);
        }

        [Fact(DisplayName = nameof(Deve_Remover_TodosItens_Corretamente_Status_EmProcessamento))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Remover_TodosItens_Corretamente_Status_EmProcessamento()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
               _fixture.RetornaDataEmissaoValida()
           );

            var itens = _fixture.RetornaListaItensValida(notaFiscal.Id, 5);
            foreach (var item in itens)
                notaFiscal.AdicionarItem(item);

            notaFiscal.ColocarEmProcessamento();

            notaFiscal.RemoverTodosItens();

            notaFiscal.Items.Should().HaveCount(0);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.EmProcessamento);
        }

        [Fact(DisplayName = nameof(Deve_Remover_TodosItens_Corretamente_Status_Erro))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Remover_TodosItens_Corretamente_Status_Erro()
        {
            var notaFiscal = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
               _fixture.RetornaDataEmissaoValida()
           );

            var itens = _fixture.RetornaListaItensValida(notaFiscal.Id, 5);
            foreach (var item in itens)
                notaFiscal.AdicionarItem(item);

            notaFiscal.DefinirErro();

            notaFiscal.RemoverTodosItens();

            notaFiscal.Items.Should().HaveCount(0);
            notaFiscal.ValoTotal.Should().Be(0);
            notaFiscal.Status.Should().Be(Domain.Enum.NotaFiscalStatus.Erro);
        }

        [Theory(DisplayName = nameof(Deve_Retornar_Erro_AoRemover_TodosItens_Corretamente_Status_Invalidos))]
        [Trait("Domain", "NotaFiscal - Entity")]
        [InlineData(NotaFiscalStatus.Cancelada)]
        [InlineData(NotaFiscalStatus.Rejeitada)]
        [InlineData(NotaFiscalStatus.Autorizada)]
        public void Deve_Retornar_Erro_AoRemover_TodosItens_Corretamente_Status_Invalidos(NotaFiscalStatus notaFiscalStatus)
        {
            var notaFicalValida = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
                _fixture.RetornaDataEmissaoValida()
                );

            var novoEmissor = _fixture.RetornaEmissorValido();
            var novaDataEmissao = _fixture.RetornaDataEmissaoValida();

            switch (notaFiscalStatus)
            {
                case NotaFiscalStatus.Cancelada:
                    notaFicalValida.Cancelar();
                    break;
                case NotaFiscalStatus.Rejeitada:
                    notaFicalValida.Rejeitar();
                    break;
                case NotaFiscalStatus.Autorizada:
                    notaFicalValida.Autorizar();
                    break;
            }

            Action action = () => notaFicalValida.RemoverTodosItens();

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Nota Fiscal não pode ser alterada");
        }

        [Theory(DisplayName = nameof(Deve_Retorna_Error_Emissor_Vazio))]
        [Trait("Domain", "NotaFiscal - Entity")]
        [InlineData(NotaFiscalStatus.Cancelada)]
        [InlineData(NotaFiscalStatus.Rejeitada)]
        [InlineData(NotaFiscalStatus.Autorizada)]
        public void Deve_Gerar_Erro_Quando_NotaFiscal_Com_Invalidos_Para_Alteracao(NotaFiscalStatus notaFiscalStatus)
        {
            var notaFicalValida = new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
                _fixture.RetornaDataEmissaoValida()
                );

            var novoEmissor = _fixture.RetornaEmissorValido();
            var novaDataEmissao = _fixture.RetornaDataEmissaoValida();

            switch (notaFiscalStatus)
            {
                case NotaFiscalStatus.Cancelada:
                    notaFicalValida.Cancelar();
                    break;
                case NotaFiscalStatus.Rejeitada:
                    notaFicalValida.Rejeitar();
                    break;
                case NotaFiscalStatus.Autorizada:
                    notaFicalValida.Autorizar();
                    break;
            }

            Action action = () => notaFicalValida.Atualizar(novoEmissor,
                novaDataEmissao);

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Nota Fiscal não pode ser alterada");
        }

        [Theory(DisplayName = nameof(Deve_Retorna_Error_Emissor_Vazio))]
        [Trait("Domain", "NotaFiscal - Entity")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Deve_Retorna_Error_Emissor_Vazio(string emissor)
        {

            Action action = () => new Domain.Entity.NotaFiscal(
                emissor,
                _fixture.RetornaDataEmissaoValida()
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Emissor should not be empty or null");

        }

        [Theory(DisplayName = nameof(Deve_Retorna_Error_DataEmissaoInvalida))]
        [Trait("Domain", "NotaFiscal - Entity")]
        [InlineData("0001-01-01")]
        public void Deve_Retorna_Error_DataEmissaoInvalida(DateTime dataEmissao)
        {
            Action action = () => new Domain.Entity.NotaFiscal(
                _fixture.RetornaEmissorValido(),
                dataEmissao
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("DataEmissao should not be null");

        }
    }
}
