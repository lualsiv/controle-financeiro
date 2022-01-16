using System;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ILancamentoWriteOnlyRepository
	{
		Task IncluiLancamentoAsync(LancamentoEntity lancamento);

		Task AtualizaLancamentoAsync(LancamentoEntity lancamento);

		Task ExcluiLancamentoAsync(int idLancamento);
	}
}

