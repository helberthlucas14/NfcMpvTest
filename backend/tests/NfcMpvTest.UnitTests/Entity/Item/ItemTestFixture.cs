using NfcMpvTest.UnitTests.Common;
using EntityDomain = NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.UnitTests.Entity.Item
{
    public class ItemTestFixture : BaseFixture
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

        public EntityDomain.NotaFiscal RetornaNotaFiscalValida()
        {
            return new EntityDomain.NotaFiscal(
                 RetornaEmissorValido(),
                 RetornaDataEmissaoValida()
             );
        }
    }

    [CollectionDefinition(nameof(ItemTestFixture))]
    public class ItemTestCollection : ICollectionFixture<ItemTestFixture>
    {
    }

}
