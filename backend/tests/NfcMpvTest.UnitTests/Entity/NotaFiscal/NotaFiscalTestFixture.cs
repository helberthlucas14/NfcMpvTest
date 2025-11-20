using NfcMpvTest.Domain.Enum;
using NfcMpvTest.UnitTests.Common;

namespace NfcMpvTest.UnitTests.Entity.NotaFiscal
{

    public class NotaFiscalTestFixture : BaseFixture
    {
        public string RetornaEmissorValido()
        {
            var emissorValido = "";

            while (emissorValido.Length < 3)
                emissorValido = Faker.Company.CompanyName();
            if (emissorValido.Length > 150)
                emissorValido = emissorValido[..150];

            return emissorValido;
        }

        public void DefinirStatusNotaFiscal(NfcMpvTest.Domain.Entity.NotaFiscal notaFiscal,
            NfcMpvTest.Domain.Enum.NotaFiscalStatus status)
        {
            switch (status)
            {
                case NotaFiscalStatus.Autorizada:
                    notaFiscal.Autorizar();
                    break;
                case NotaFiscalStatus.Cancelada:
                    notaFiscal.Cancelar();
                    break;
                case NotaFiscalStatus.Rejeitada:
                    notaFiscal.Rejeitar();
                    break;
                case NotaFiscalStatus.EmProcessamento:
                    notaFiscal.ColocarEmProcessamento();
                    break;
                case NotaFiscalStatus.Erro:
                    notaFiscal.DefinirErro();
                    break;
                default:
                    break;
            }
        }

        public DateTime RetornaDataEmissaoValida()
        {
            return Faker.Date.Past();
        }

        public List<NfcMpvTest.Domain.Entity.Item> RetornaListaItensValida(Guid notafiscalId, int? tamanho = 1)
        {
            var itens = new List<NfcMpvTest.Domain.Entity.Item>();
            for (int i = 0; i < tamanho; i++)
            {
                var item = new NfcMpvTest.Domain.Entity.Item(
                   notafiscalId,
                    Faker.Commerce.ProductName(),
                    Faker.Random.Decimal(1, 1000),
                    Guid.NewGuid()
                );
                itens.Add(item);
            }
            return itens;
        }
    }

    [CollectionDefinition(nameof(NotaFiscalTestFixture))]
    public class NotaFiscalTestCollection : ICollectionFixture<NotaFiscalTestFixture>
    {
    }
}
