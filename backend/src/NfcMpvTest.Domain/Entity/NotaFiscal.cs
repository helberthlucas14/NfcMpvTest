using NfcMpvTest.Domain.Enum;
using NfcMpvTest.Domain.Exceptions;

namespace NfcMpvTest.Domain.Entity
{
    public class NotaFiscal : Entity
    {
        public string Emissor { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public List<Item> Items { get; private set; } = new();
        public decimal ValoTotal => CalcularValorTotal();
        public NotaFiscalStatus Status { get; private set; }

        public NotaFiscal(string emissor, DateTime dataEmissao)
        {
            Emissor = emissor;
            DataEmissao = dataEmissao;
            Status = NotaFiscalStatus.Emitida;
            Validate();
        }

        public virtual void AdicionarItem(Item item)
        {
            VerificaStatusDaNotaFiscal();
            Items.Add(item);
        }

        public void RemoverItem(Item item)
        {
            VerificaStatusDaNotaFiscal();
            Items.Remove(item);
        }

        public void RemoverTodosItens()
        {
            VerificaStatusDaNotaFiscal();
            Items.Clear();
        }

        public void Atualizar(string? emissor = null, DateTime? dataEmissao = null)
        {
            Emissor = emissor ?? Emissor;
            DataEmissao = dataEmissao ?? DataEmissao;
            VerificaStatusDaNotaFiscal();
            Validate();
        }

        public void Autorizar()
        {
            if (Status == NotaFiscalStatus.Cancelada ||
                Status == NotaFiscalStatus.Erro)
                throw new EntityValidationException("Nota Fiscal não pode ser autorizada.");

            if (Status == NotaFiscalStatus.EmProcessamento || Status == NotaFiscalStatus.Autorizada)
                return;

            Status = NotaFiscalStatus.Autorizada;
            Validate();
        }

        public void Cancelar()
        {
            if (Status == NotaFiscalStatus.Rejeitada)
                throw new EntityValidationException("Nota Fiscal rejeitada não pode ser autorizada.");

            if (Status == NotaFiscalStatus.Cancelada)
                return;

            Status = NotaFiscalStatus.Cancelada;

            Validate();
        }

        public void Rejeitar()
        {
            if (Status == NotaFiscalStatus.Cancelada ||
                Status == NotaFiscalStatus.Autorizada)
                throw new EntityValidationException("Nota fiscal não pode ser rejeitada.");

            if (Status == NotaFiscalStatus.Rejeitada)
                return;

            Status = NotaFiscalStatus.Rejeitada;
            Validate();
        }

        public void DefinirErro()
        {
            if (Status != NotaFiscalStatus.EmProcessamento)
                throw new EntityValidationException("Não e possivel Definir Erro apenas para notas ficais em processamento.");

            Status = NotaFiscalStatus.Erro;

            Validate();
        }

        public void ColocarEmProcessamento()
        {
            if ((Status != NotaFiscalStatus.Emitida) && (Status != NotaFiscalStatus.Erro))
                throw new EntityValidationException("Nota fiscal não pode ser colocada em processamento.");

            if (Status == NotaFiscalStatus.EmProcessamento)
                return;

            Status = NotaFiscalStatus.EmProcessamento;
            Validate();
        }

        public virtual decimal CalcularValorTotal() => Items.Sum(i => i.Valor);

        private void VerificaStatusDaNotaFiscal()
        {
            if (Status == NotaFiscalStatus.Autorizada ||
                Status == NotaFiscalStatus.Cancelada ||
                Status == NotaFiscalStatus.Rejeitada)
                throw new EntityValidationException("Nota Fiscal não pode ser alterada");
        }

        private void Validate()
        {
            Validation.DomainValidation.NotNullOrEmpty(Emissor, nameof(Emissor));
            Validation.DomainValidation.MinLength(Emissor, 2, nameof(Emissor));
            Validation.DomainValidation.MaxLength(Emissor, 150, nameof(Emissor));
            Validation.DomainValidation.NotNull(DataEmissao, nameof(DataEmissao));
        }

    }
}
