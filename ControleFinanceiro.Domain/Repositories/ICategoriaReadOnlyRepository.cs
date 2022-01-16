using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ICategoriaReadOnlyRepository
	{
		Task<CategoriaEntity> RetornaCategoriaAsync(int idCategoria);		

		Task<bool> CategoriaExisteAsync(int idCategoria);

		Task<IEnumerable<CategoriaEntity>> ListaCategoriasAsync();
	}
}

