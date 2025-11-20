using MediatR;
using NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal
{
    public class CriarNotaFiscalRequest
        : IRequest<NotaFiscalResponse>
    {
        public string Emissor { get; set; }
        public DateTime DataEmissao { get; set; }
        public List<ItemRequest> Itens { get; set; } = new();
        public CriarNotaFiscalRequest(string emissor,
            DateTime dataEmissao,
            List<ItemRequest> itens)
        {
            Emissor = emissor;
            DataEmissao = dataEmissao;
            Itens = itens;
        }
    }
}
