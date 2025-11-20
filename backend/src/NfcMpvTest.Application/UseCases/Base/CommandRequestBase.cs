using MediatR;

namespace NfcMpvTest.Application.UseCases.Base
{
    public abstract class CommandRequestBase<TResponse> : IRequest<TResponse>
    {
        public Guid CorrelationId { get; set; }
        public string? JobId { get; set; }
    }
}
