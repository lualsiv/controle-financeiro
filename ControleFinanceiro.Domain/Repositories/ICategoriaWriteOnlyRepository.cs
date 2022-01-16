using System;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ICategoriaWriteOnlyRepository
	{
		Task IncluiCategoriaAsync(CategoriaEntity categoria);		

		Task AtualizaCategoriaAsync(CategoriaEntity categoria);

		Task ExcluiCategoriaAsync(int idCategoria);
	}
}

