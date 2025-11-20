namespace NfcMpvTest.Application.Logging
{
    public interface IApplicationLogging
    {
        void LogIniciado(Guid correlationId, string operation, string? jobId = null);
        void LogCompleto(Guid correlationId, string operation, double durationMs, string? jobId = null);
        void LogFalha(Guid correlationId, string operation, double durationMs, Exception ex, string? jobId = null);
    }
}
