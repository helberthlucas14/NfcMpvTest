namespace NfcMpvTest.Domain.Entity
{
    public class Item : Entity
    {
        public Guid NotaFiscalId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Guid ProdutoCodigo { get; set; }

        public Item(Guid notaFiscalId, string descricao, decimal valor, Guid produtoCodigo)
        {
            NotaFiscalId = notaFiscalId;
            Descricao = descricao;
            Valor = valor;
            ProdutoCodigo = produtoCodigo;
            Validate();
        }

        public void Atualizar(
            string? descricao = null,
            decimal? valor = null,
            Guid? produtoCodigo = null)
        {
            Descricao = descricao ?? Descricao;
            Valor = valor ?? Valor;
            ProdutoCodigo = produtoCodigo ?? ProdutoCodigo;
            Validate();
        }

        private void Validate()
        {
            Validation.DomainValidation.NotNullOrEmpty(Descricao, nameof(Descricao));
            Validation.DomainValidation.MinLength(Descricao, 3, nameof(Descricao));
            Validation.DomainValidation.MaxLength(Descricao, 255, nameof(Descricao));

            Validation.DomainValidation.InvalidAtributeMinValue(Valor, nameof(Valor));
            Validation.DomainValidation.NotNull(Valor, nameof(Valor));
            Validation.DomainValidation.NotNull(ProdutoCodigo, nameof(ProdutoCodigo));
        }
    }
}
