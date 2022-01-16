using System;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using Microsoft.Extensions.Logging;

namespace ControleFinanceiro.Application.Services
{
	public class BalancoService : IBalancoService
	{
        private readonly ILancamentoReadOnlyRepository lancamentoReadOnly;
        private readonly ICategoriaReadOnlyRepository categoriaReadOnly;
        private readonly ILogger logger;

        public BalancoService(ILancamentoReadOnlyRepository lancamentoReadOnly
            ,ILoggerFactory loggerFactory
            ,ICategoriaReadOnlyRepository categoriaReadOnly)
		{
            this.lancamentoReadOnly = lancamentoReadOnly ?? throw new ArgumentNullException(nameof(lancamentoReadOnly));
            this.categoriaReadOnly = categoriaReadOnly ?? throw new ArgumentNullException(nameof(categoriaReadOnly));

            this.logger = loggerFactory?.CreateLogger<BalancoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<Balanco> RetornaBalanco(BalancoFiltro filtro)
        {
            CategoriaEntity categoria = null;
            if (filtro.idCategoria.HasValue)
                categoria = await categoriaReadOnly.RetornaCategoriaAsync(filtro.idCategoria.Value);

            var receita = await lancamentoReadOnly.ListaLancamentosReceitaAsync(filtro);
            var despesa = await lancamentoReadOnly.ListaLancamentosDespesaAsync(filtro);

            Balanco balanco = new Balanco
            {
                categoria = categoria,
                receita = receita.Sum(r => r.valor),
                despesa = despesa.Sum(d=> d.valor)
            };

            return balanco;
        }
    }
}

