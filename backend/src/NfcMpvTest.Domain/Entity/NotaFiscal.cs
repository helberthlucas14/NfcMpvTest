namespace NfcMpvTest.Domain.Entity
{
    public class NotaFiscal : Entity
    {
        public string Emissor { get; set; }
        public DateTime DataEmissao { get; set; }
        public List<Item> Items { get; private set; } = new();
        public decimal ValoTotal => CalcularValorTotal();

        public NotaFiscal(string emissor, DateTime dataEmissao)
        {
            Emissor = emissor;
            DataEmissao = dataEmissao;
            Validate();
        }

        public virtual void AdicionarItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoverItem(Item item)
        {
            Items.Remove(item);
        }

        public void RemoverTodosItens()
        {
            Items.Clear();
        }

        public void Atualizar(string? emissor = null, DateTime? dataEmissao = null)
        {
            Emissor = emissor ?? Emissor;
            DataEmissao = dataEmissao ?? DataEmissao;
            Validate();
        }

        public virtual decimal CalcularValorTotal() => Items.Sum(i => i.Valor);

        private void Validate()
        {
            Validation.DomainValidation.NotNullOrEmpty(Emissor, nameof(Emissor));
            Validation.DomainValidation.MinLength(Emissor, 2, nameof(Emissor));
            Validation.DomainValidation.MaxLength(Emissor, 150, nameof(Emissor));
            Validation.DomainValidation.NotNull(DataEmissao, nameof(DataEmissao));
        }
    }
}
