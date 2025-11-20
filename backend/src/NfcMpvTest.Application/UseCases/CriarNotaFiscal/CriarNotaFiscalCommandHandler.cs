using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using NfcMpvTest.Application.Logging;
using NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common;
using NfcMpvTest.Domain.Entity;
using NfcMpvTest.Domain.Repository;
using NfcMpvTest.Domain.Services;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal
{
    public class CriarNotaFiscalCommandHandler
        : IRequestHandler<CriarNotaFiscalCommand, NotaFiscalResponse>
    {
        private readonly INotaFiscalService _criarNotaFiscalService;
        private readonly IUnitOfWork uof;
        private readonly ILogger<CriarNotaFiscalCommandHandler> _logger;
        private readonly IApplicationLogging _loggingHelper;

        public CriarNotaFiscalCommandHandler(
            INotaFiscalService criarNotaFiscalService,
            IUnitOfWork unitOfWork,
            ILogger<CriarNotaFiscalCommandHandler> logger,
            IApplicationLogging loggingHelper)
        {
            _criarNotaFiscalService = criarNotaFiscalService;
            uof = unitOfWork;
            _logger = logger;
            _loggingHelper = loggingHelper;
        }

        public async Task<NotaFiscalResponse> Handle(CriarNotaFiscalCommand request, CancellationToken cancellationToken)
        {
            var correlationId = request.CorrelationId;

            var startTime = DateTime.UtcNow;
            try
            {
                _loggingHelper.LogIniciado(correlationId, nameof(CriarNotaFiscalCommand), request.JobId);

                var notaFiscal = new NotaFiscal(request.Emissor, request.DataEmissao);

                var entity = await _criarNotaFiscalService.RegisterAsync(notaFiscal);

                await uof.CommitAsync(cancellationToken);

                var durationMs = (DateTime.UtcNow - startTime).TotalMilliseconds;

                _loggingHelper.LogCompleto(correlationId, nameof(CriarNotaFiscalCommand), durationMs, request.JobId);

                return NotaFiscalResponse.FromMember(entity);
            }
            catch (Exception ex)
            {
                var durationMs = (DateTime.UtcNow - startTime).TotalMilliseconds;
                _loggingHelper.LogFalha(correlationId, nameof(CriarNotaFiscalCommand), durationMs, ex, request.JobId);
                throw;
            }

        }
    }
}
