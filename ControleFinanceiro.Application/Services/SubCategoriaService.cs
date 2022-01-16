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
	public class SubCategoriaService : ISubCategoriaService
	{
        private readonly ISubCategoriaReadOnlyRepository subCategoriaReadOnly;
        private readonly ISubCategoriaWriteOnlyRepository subCategoriaWriteOnly;
        private readonly ILogger logger;

        public SubCategoriaService(ISubCategoriaReadOnlyRepository subCategoriaReadOnly
            , ISubCategoriaWriteOnlyRepository subCategoriaWriteOnly
            , ILoggerFactory loggerFactory)
        {
            this.subCategoriaReadOnly = subCategoriaReadOnly ?? throw new ArgumentNullException(nameof(subCategoriaReadOnly));
            this.subCategoriaWriteOnly = subCategoriaWriteOnly ?? throw new ArgumentNullException(nameof(subCategoriaWriteOnly));

            this.logger = loggerFactory?.CreateLogger<CategoriaService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task AtualizaSubCategoriaAsync(SubCategoriaEntity subCategoria)
        {
            if (subCategoria == null)
                throw new ArgumentNullException(nameof(subCategoria));

            ValidationHelper.ThrowValidationExceptionIfNotValid(subCategoria);

            if (!await SubCategoriaExisteAsync(subCategoria.id))
                throw new SubCategoriaCoreException(SubCategoriaCoreError.SubCategoriaNaoEncontrado);

            await subCategoriaWriteOnly.AtualizaSubCategoriaAsync(subCategoria);
        }

        public async Task<bool> SubCategoriaExisteAsync(int idSubCategoria)
        {
            var result = await subCategoriaReadOnly.SubCategoriaExisteAsync(idSubCategoria);

            return result;
        }

        public async Task ExcluiSubCategoriaAsync(int idSubCategoria)
        {
            if (!await SubCategoriaExisteAsync(idSubCategoria))
                throw new SubCategoriaCoreException(SubCategoriaCoreError.SubCategoriaNaoEncontrado);

            await subCategoriaWriteOnly.ExcluiSubCategoriaAsync(idSubCategoria);
        }

        public async Task IncluiSubCategoriaAsync(SubCategoriaEntity subCategoria)
        {
            if (subCategoria == null)
                throw new ArgumentNullException(nameof(subCategoria));

            ValidationHelper.ThrowValidationExceptionIfNotValid(subCategoria);

            // Utilizar a gravação de logInformation somente se for realmente necessário
            // ter um acompanhamento de tudo que esta acontecendo
            logger.LogInformation("Iniciando gravação da SubCategoria...");
            await this.subCategoriaWriteOnly.IncluiSubCategoriaAsync(subCategoria);
            logger.LogInformation("SubCategoria gravado.");
        }

        public async Task<SubCategoriaEntity> RetornaSubCategoriaAsync(int idSubCategoria)
        {
            var result = await subCategoriaReadOnly.RetornaSubCategoriaAsync(idSubCategoria);

            return result;
        }

        public async Task<IEnumerable<SubCategoriaEntity>> RetornaSubCategoriasAsync()
        {
            var result = await subCategoriaReadOnly.ListaSubCategoriasAsync();

            return result;
        }
    }
}

