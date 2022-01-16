using System;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Services
{
	public interface IBalancoService
	{
		Task<Balanco> RetornaBalanco(BalancoFiltro filtro);
		
	}
}

