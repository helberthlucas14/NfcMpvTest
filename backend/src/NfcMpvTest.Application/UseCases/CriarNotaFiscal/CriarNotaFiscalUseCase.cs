using MediatR;
using Microsoft.Extensions.Logging;
using NfcMpvTest.Application.UseCases.CriarNotaFiscal.Common;
using NfcMpvTest.Domain.Entity;
using NfcMpvTest.Domain.Repository;
using NfcMpvTest.Domain.Services;
using NfcMpvTest.Infra.CrossCuttingCommons.Commons;

namespace NfcMpvTest.Application.UseCases.CriarNotaFiscal
{
    public class CriarNotaFiscalUseCase : IRequestHandler<CriarNotaFiscalRequest, NotaFiscalResponse>
    {
        private readonly INotaFiscalService _criarNotaFiscalService;
        private readonly IUnitOfWork uof;
        private readonly ILogger<CriarNotaFiscalUseCase> _logger;

        public CriarNotaFiscalUseCase(
            INotaFiscalService criarNotaFiscalService,
            IUnitOfWork unitOfWork,
            ILogger<CriarNotaFiscalUseCase> logger)
        {
            _criarNotaFiscalService = criarNotaFiscalService;
            uof = unitOfWork;
            _logger = logger;
        }

        public async Task<NotaFiscalResponse> Handle(CriarNotaFiscalRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando o processo de criação da Nota Fiscal para o emissor: {object}", request.ToJson());

            var notaFiscal = new NotaFiscal(request.Emissor, request.DataEmissao);

            var entity = await _criarNotaFiscalService.RegisterAsync(notaFiscal);

            await uof.CommitAsync(cancellationToken);

            return NotaFiscalResponse.FromMember(entity);
        }
    }
}
