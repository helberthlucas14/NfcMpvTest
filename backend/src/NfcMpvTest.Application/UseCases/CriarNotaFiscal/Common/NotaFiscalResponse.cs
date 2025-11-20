
using NfcMpvTest.Domain.Entity;
using NfcMpvTest.Domain.Enum;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common
{
    public class NotaFiscalResponse
    {
        public Guid Id { get; set; }
        public string Emissor { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal ValoTotal { get; set; }
        public NotaFiscalStatus Status { get; set; }
        public List<ItemResponse> Items { get; set; } = new();
        public static NotaFiscalResponse FromMember(NotaFiscal notaFiscal)
        {
            return new NotaFiscalResponse
            {
                Id = notaFiscal.Id,
                Emissor = notaFiscal.Emissor,
                DataEmissao = notaFiscal.DataEmissao,
                ValoTotal = notaFiscal.ValoTotal,
                Status = notaFiscal.Status,
                Items = notaFiscal.Items.Select(item => ItemResponse.FromMember(item)).ToList()
            };
        }
    }
}
