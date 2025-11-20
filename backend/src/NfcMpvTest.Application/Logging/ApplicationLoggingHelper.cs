using Microsoft.Extensions.Logging;

namespace NfcMpvTest.Application.Logging
{
    public class ApplicationLoggingHelper : IApplicationLogging
    {
        private readonly ILogger _logger;
        public ApplicationLoggingHelper(ILogger logger) => _logger = logger;

        public void LogIniciado(Guid correlationId, string operation, string? jobId = null)
        {
            _logger.LogInformation("Iniciado {Operation} - {@LogObject}", operation, new
            {
                correlationId,
                jobId,
                operation,
                status = "Iniciado"
            });
        }

        public void LogCompleto(Guid correlationId, string operation, double durationMs, string? jobId = null)
        {
            _logger.LogInformation("Completo {Operation} - {@LogObject}", operation, new
            {
                correlationId,
                jobId,
                operation,
                status = "Completa",
                durationMs
            });
        }

        public void LogFalha(Guid correlationId, string operation, double durationMs, Exception ex, string? jobId = null)
        {
            _logger.LogError(ex, "Falhou {Operation} - {@LogObject}", operation, new
            {
                correlationId,
                jobId,
                operation,
                status = "Falhou",
                durationMs
            });
        }
    }

}
