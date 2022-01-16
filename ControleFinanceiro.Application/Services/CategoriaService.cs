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
	public class CategoriaService : ICategoriaService
	{
        private readonly ICategoriaReadOnlyRepository categoriaReadOnlyRepository;
        private readonly ICategoriaWriteOnlyRepository categoriaWriteOnlyRepository;
        private readonly ILogger logger;

        public CategoriaService(ICategoriaReadOnlyRepository categoriaReadOnly
            ,ICategoriaWriteOnlyRepository categoriaWriteOnly
            ,ILoggerFactory loggerFactory)
		{
            this.categoriaReadOnlyRepository = categoriaReadOnly ?? throw new ArgumentNullException(nameof(categoriaReadOnly));
            this.categoriaWriteOnlyRepository = categoriaWriteOnly ?? throw new ArgumentNullException(nameof(categoriaWriteOnly));

            this.logger = loggerFactory?.CreateLogger<CategoriaService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task AtualizaCategoriaAsync(CategoriaEntity categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            ValidationHelper.ThrowValidationExceptionIfNotValid(categoria);

            if (!await CategoriaExisteAsync(categoria.id))
                throw new CategoriaCoreException(CategoriaCoreError.CategoriaNaoEncontrado);

            await categoriaWriteOnlyRepository.AtualizaCategoriaAsync(categoria);
        }

        public async Task<bool> CategoriaExisteAsync(int idCategoria)
        {
            var result = await categoriaReadOnlyRepository.CategoriaExisteAsync(idCategoria);

            return result;
        }

        public async Task ExcluiCategoriaAsync(int idCategoria)
        {
            if (!await CategoriaExisteAsync(idCategoria))
                throw new CategoriaCoreException(CategoriaCoreError.CategoriaNaoEncontrado);

            await categoriaWriteOnlyRepository.ExcluiCategoriaAsync(idCategoria);
        }

        public async Task IncluiCategoriaAsync(CategoriaEntity categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            ValidationHelper.ThrowValidationExceptionIfNotValid(categoria);

            // Utilizar a gravação de logInformation somente se for realmente necessário
            // ter um acompanhamento de tudo que esta acontecendo
            logger.LogInformation("Iniciando gravação da Categoria...");
            await this.categoriaWriteOnlyRepository.IncluiCategoriaAsync(categoria);
            logger.LogInformation("Categoria gravado.");
        }

        public async Task<CategoriaEntity> RetornaCategoriaAsync(int idCategoria)
        {
            var categoria = await categoriaReadOnlyRepository.RetornaCategoriaAsync(idCategoria);

            return categoria;
        }

        public async Task<IEnumerable<CategoriaEntity>> RetornaCategoriasAsync()
        {
            var categorias = await categoriaReadOnlyRepository.ListaCategoriasAsync();

            return categorias;
        }
    }
}

