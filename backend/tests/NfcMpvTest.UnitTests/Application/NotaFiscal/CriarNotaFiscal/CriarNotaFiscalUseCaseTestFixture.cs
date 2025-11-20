using NfcMpvTest.Application.UseCases.CriarNotaFiscal;
using NfcMpvTest.UnitTests.Common;
using DomainEntity = NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.UnitTests.Application.NotaFiscal.CriarNotaFiscal
{
    public class CriarNotaFiscalUseCaseTestFixture : BaseFixture
    {
        public string RetonraDescricaoValido()
        {
            var descricaoValida = "";
            while (descricaoValida.Length < 3)
                descricaoValida = Faker.Commerce.ProductName();
            if (descricaoValida.Length > 255)
                descricaoValida = descricaoValida[..255];
            return descricaoValida;
        }

        public decimal RetornaValorValido()
        {
            return Faker.Random.Decimal(0, 10000);
        }

        public Guid RetornaProdutoCodigoValido()
        {
            return Guid.NewGuid();
        }

        public string RetornaEmissorValido()
        {
            var emissorValido = "";

            while (emissorValido.Length < 3)
                emissorValido = Faker.Company.CompanyName();
            if (emissorValido.Length > 150)
                emissorValido = emissorValido[..150];

            return emissorValido;
        }

        public DateTime RetornaDataEmissaoValida()
        {
            return Faker.Date.Past();
        }
        public List<ItemRequest> RetornaItemRequestList(int? tamanho = 1)
        {
            var itens = new List<ItemRequest>();
            for (int i = 0; i < tamanho; i++)
            {
                var item = new ItemRequest(
                    Faker.Commerce.ProductDescription(),
                    Faker.Random.Decimal(1, 1000),
                    Guid.NewGuid()
                );
                itens.Add(item);
            }
            return itens;
        }

        public List<ItemRequest> RetornaItemRequestPorUmaItemList(List<DomainEntity.Item> items)
        {
            var itensRequest = new List<ItemRequest>();
            foreach (var item in items)
            {
                itensRequest.Add
                    (new ItemRequest(
                        item.Descricao,
                        item.Valor,
                        item.ProdutoCodigo));
            }
            ;

            return itensRequest;
        }

        public CriarNotaFiscalCommand RetornaNotaFiscalRequestValida(int? items = 1)
        {
            return new CriarNotaFiscalCommand(
                 RetornaEmissorValido(),
                 RetornaDataEmissaoValida(),
                 RetornaItemRequestList(items)
             );
        }

        public DomainEntity.NotaFiscal RetornaNotaFiscalValida(int? items = 1)
        {
            var notaFiscal = new DomainEntity.NotaFiscal(
                RetornaEmissorValido(),
                RetornaDataEmissaoValida()
            );

            for (int i = 0; i < items; i++)
            {
                var item = new DomainEntity.Item(
                    notaFiscal.Id,
                    RetonraDescricaoValido(),
                    RetornaValorValido(),
                    RetornaProdutoCodigoValido()
                );
                notaFiscal.AdicionarItem(item);
            }
            return notaFiscal;
        }
    }
}
