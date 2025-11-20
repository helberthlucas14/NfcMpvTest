using NfcMpvTest.Application.UseCases.Base;
using NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal
{
    public class CriarNotaFiscalCommand : CommandRequestBase<NotaFiscalResponse>
    {
        public string Emissor { get; set; }
        public DateTime DataEmissao { get; set; }
        public List<ItemRequest> Itens { get; set; } = new();
        public CriarNotaFiscalCommand(string emissor,
            DateTime dataEmissao,
            List<ItemRequest> itens) : base()
        {
            Emissor = emissor;
            DataEmissao = dataEmissao;
            Itens = itens;
        }
    }
}
