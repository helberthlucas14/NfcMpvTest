using FluentAssertions;
using NfcMpvTest.Domain.Exceptions;


namespace NfcMpvTest.UnitTests.Entity.Item
{
    [Collection(nameof(ItemTestFixture))]
    public class ItemTest
    {
        private readonly ItemTestFixture _fixture;
        public ItemTest(ItemTestFixture fixture) => _fixture = fixture;

        [Fact(DisplayName = nameof(Deve_Criar_Item_Corretamente))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Criar_Item_Corretamente()
        {
            var notaFiscal = _fixture.RetornaNotaFiscalValida();
            var descrisao = _fixture.RetornaEmissorValido();
            var valor = _fixture.RetornaValorValido();
            var produto = _fixture.RetornaProdutoCodigoValido();

            var item = new Domain.Entity.Item(
                notaFiscal.Id,
                descrisao,
                valor,
                produto
            );


            item.NotaFiscalId.Should().Be(notaFiscal.Id);
            item.Descricao.Should().Be(descrisao);
            item.Valor.Should().Be(valor);
            item.ProdutoCodigo.Should().Be(produto);
        }

        [Fact(DisplayName = nameof(Deve_Criar_Item_Corretamente))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Criar_Item_Valor_0_Corretamente()
        {
            var notaFiscal = _fixture.RetornaNotaFiscalValida();
            var descrisao = _fixture.RetornaEmissorValido();
            var valor = 0;
            var produto = _fixture.RetornaProdutoCodigoValido();


            var item = new Domain.Entity.Item(
                notaFiscal.Id,
                descrisao,
                valor,
                produto
            );


            item.NotaFiscalId.Should().Be(notaFiscal.Id);
            item.Descricao.Should().Be(descrisao);
            item.Valor.Should().Be(valor);
            item.ProdutoCodigo.Should().Be(produto);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_Descricao_Corretamente))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Atualizar_Descricao_Corretamente()
        {
            var notaFiscal = _fixture.RetornaNotaFiscalValida();
            var descrisao = _fixture.RetornaEmissorValido();
            var valor = 0;
            var produto = _fixture.RetornaProdutoCodigoValido();

            var item = new Domain.Entity.Item(
                notaFiscal.Id,
                descrisao,
                valor,
                produto
            );

            var novaDescrisao = _fixture.RetornaEmissorValido();

            item.Atualizar(novaDescrisao);

            item.NotaFiscalId.Should().Be(notaFiscal.Id);
            item.Descricao.Should().Be(novaDescrisao);
            item.Valor.Should().Be(valor);
            item.ProdutoCodigo.Should().Be(produto);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_Valor_Corretamente))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Atualizar_Valor_Corretamente()
        {
            var notaFiscal = _fixture.RetornaNotaFiscalValida();
            var descrisao = _fixture.RetornaEmissorValido();
            var valor = 0;
            var produto = _fixture.RetornaProdutoCodigoValido();

            var item = new Domain.Entity.Item(
                notaFiscal.Id,
                descrisao,
                valor,
                produto
            );

            var novoValor = _fixture.RetornaValorValido();

            item.Atualizar(valor: novoValor);

            item.NotaFiscalId.Should().Be(notaFiscal.Id);
            item.Descricao.Should().Be(descrisao);
            item.Valor.Should().Be(novoValor);
            item.ProdutoCodigo.Should().Be(produto);
        }

        [Fact(DisplayName = nameof(Deve_Atualizar_ProdutoCodigo_Corretamente))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Atualizar_ProdutoCodigo_Corretamente()
        {
            var notaFiscal = _fixture.RetornaNotaFiscalValida();
            var descrisao = _fixture.RetornaEmissorValido();
            var valor = 0;
            var produto = _fixture.RetornaProdutoCodigoValido();

            var item = new Domain.Entity.Item(
                notaFiscal.Id,
                descrisao,
                valor,
                produto
            );

            var produtoCodigo = _fixture.RetornaProdutoCodigoValido();

            item.Atualizar(produtoCodigo: produtoCodigo);

            item.NotaFiscalId.Should().Be(notaFiscal.Id);
            item.Descricao.Should().Be(descrisao);
            item.Valor.Should().Be(valor);
            item.ProdutoCodigo.Should().Be(produtoCodigo);
        }



        [Theory(DisplayName = nameof(Deve_Retorna_Error_Descricao))]
        [Trait("Domain", "Item - Entity")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Deve_Retorna_Error_Descricao(string descricao)
        {
            var notafiscalValida = _fixture.RetornaNotaFiscalValida();

            Action action = () => new Domain.Entity.Item(
                notafiscalValida.Id,
                descricao,
                _fixture.RetornaValorValido(),
                _fixture.RetornaProdutoCodigoValido()
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Descricao should not be empty or null");
        }


        [Theory(DisplayName = nameof(Deve_Retorna_Error_TamanhoMenor_Que_3_Caracteres))]
        [Trait("Domain", "Item - Entity")]
        [InlineData("1")]
        [InlineData("12")]
        public void Deve_Retorna_Error_TamanhoMenor_Que_3_Caracteres(string descricao)
        {
            var notafiscalValida = _fixture.RetornaNotaFiscalValida();

            Action action = () => new Domain.Entity.Item(
                notafiscalValida.Id,
                descricao,
                _fixture.RetornaValorValido(),
                _fixture.RetornaProdutoCodigoValido()
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Descricao should be less than 3 characters long");
        }

        [Fact(DisplayName = nameof(Deve_Retorna_Error_TamanhoMaior_Que_255_Caracteres))]
        [Trait("Domain", "NotaFiscal - Entity")]
        public void Deve_Retorna_Error_TamanhoMaior_Que_255_Caracteres()
        {
            var notafiscalValida = _fixture.RetornaNotaFiscalValida();
            var descrisaoInvalida = String.Join(null, Enumerable.Range(251, 300).Select(_ => "a").ToArray());

            Action action = () => new Domain.Entity.Item(
                notafiscalValida.Id,
                descrisaoInvalida,
                _fixture.RetornaValorValido(),
                _fixture.RetornaProdutoCodigoValido()
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Descricao should be greater than 255 characters long");
        }

        [Fact(DisplayName = nameof(Deve_Retorna_Error_TamanhoMaior_Que_255_Caracteres))]
        [Trait("Domain", "Item - Entity")]
        public void Deve_Retorna_Error_Valor_Negativo()
        {
            var notafiscalValida = _fixture.RetornaNotaFiscalValida();
            var valorInvalido = -100;

            Action action = () => new Domain.Entity.Item(
                notafiscalValida.Id,
                _fixture.RetornaEmissorValido(),
                 valorInvalido,
                _fixture.RetornaProdutoCodigoValido()
                );

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Valor should be greater than 0.");
        }
    }
}
