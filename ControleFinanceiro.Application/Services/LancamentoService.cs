using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Exceptions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using Microsoft.Extensions.Logging;
using Otc.Validations.Helpers;

namespace ControleFinanceiro.Application.Services
{
	public class LancamentoService : ILancamentoService
	{
        private readonly ILancamentoReadOnlyRepository lancamentoReadOnly;
        private readonly ILancamentoWriteOnlyRepository lancamentoWriteOnly;
        private readonly ILogger logger;

        public LancamentoService(ILancamentoReadOnlyRepository lancamentoReadOnly
            ,ILancamentoWriteOnlyRepository lancamentoWriteOnly
            , ILoggerFactory loggerFactory)
		{
            this.lancamentoReadOnly = lancamentoReadOnly ?? throw new ArgumentNullException(nameof(lancamentoReadOnly));
            this.lancamentoWriteOnly = lancamentoWriteOnly ?? throw new ArgumentNullException(nameof(lancamentoWriteOnly));

            this.logger = loggerFactory?.CreateLogger<LancamentoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task AtualizaLancamentoAsync(LancamentoEntity lancamento)
        {
            if (lancamento == null)
                throw new ArgumentNullException(nameof(lancamento));

            ValidationHelper.ThrowValidationExceptionIfNotValid(lancamento);

            if (!await LancamentoExisteAsync(lancamento.id))
                throw new LancamentoCoreException(LancamentoCoreError.LancamentoNaoEncontrado);

            await lancamentoWriteOnly.AtualizaLancamentoAsync(lancamento);
        }

        public async Task<bool> LancamentoExisteAsync(int idLancamento)
        {
            var result = await lancamentoReadOnly.LancamentoExisteAsync(idLancamento);

            return result;
        }

        public async Task ExcluiLancamentoAsync(int idLancamento)
        {
            if (!await LancamentoExisteAsync(idLancamento))
                throw new LancamentoCoreException(LancamentoCoreError.LancamentoNaoEncontrado);

            await lancamentoWriteOnly.ExcluiLancamentoAsync(idLancamento);
        }

        public async Task IncluiLancamentoAsync(LancamentoEntity lancamento)
        {
            if (lancamento == null)
                throw new ArgumentNullException(nameof(lancamento));

            ValidationHelper.ThrowValidationExceptionIfNotValid(lancamento);

            // Utilizar a gravação de logInformation somente se for realmente necessário
            // ter um acompanhamento de tudo que esta acontecendo
            logger.LogInformation("Iniciando gravação do Lançamento...");
            await this.lancamentoWriteOnly.IncluiLancamentoAsync(lancamento);
            logger.LogInformation("Lançamento gravado.");
        }

        public async Task<LancamentoEntity> RetornaLancamentoAsync(int idLancamento)
        {
            var result = await lancamentoReadOnly.RetornaLancamentoAsync(idLancamento);

            return result;
        }

        public async Task<IEnumerable<LancamentoEntity>> RetornaLancamentosAsync()
        {
            var result = await lancamentoReadOnly.ListaLancamentosAsync();

            return result;
        }
    }
}

