namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common
{
    public class ItemResponse
    {
        public Guid NotaFiscalId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Guid ProdutoCodigo { get; set; }
        
        public static ItemResponse FromMember(Domain.Entity.Item item)
        {
            return new ItemResponse()
            {
                NotaFiscalId = item.NotaFiscalId,
                Descricao = item.Descricao,
                Valor = item.Valor,
                ProdutoCodigo = item.ProdutoCodigo
            };
        }
    }
}
