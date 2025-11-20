using NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal
{
    public class ItemRequest
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Guid ProdutoCodigo { get; set; }

        public ItemRequest(string descricao, decimal valor, Guid produtoCodigo)
        {
            Descricao = descricao;
            Valor = valor;
            ProdutoCodigo = produtoCodigo;
        }
    }
}
