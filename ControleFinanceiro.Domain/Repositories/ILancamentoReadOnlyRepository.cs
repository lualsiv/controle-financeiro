using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ILancamentoReadOnlyRepository
	{
		Task<LancamentoEntity> RetornaLancamentoAsync(int idLancamento);

		Task<IEnumerable<LancamentoEntity>> ListaLancamentosAsync();
		Task<IEnumerable<LancamentoEntity>> ListaLancamentosDespesaAsync(BalancoFiltro filtro);
		Task<IEnumerable<LancamentoEntity>> ListaLancamentosReceitaAsync(BalancoFiltro filtro);
		Task<bool> LancamentoExisteAsync(int idLancamento);		
	}
}

