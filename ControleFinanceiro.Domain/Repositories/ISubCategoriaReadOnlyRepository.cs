using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ISubCategoriaReadOnlyRepository
	{
		Task<SubCategoriaEntity> RetornaSubCategoriaAsync(int idSubCategoria);		

		Task<bool> SubCategoriaExisteAsync(int idSubCategoria);

		Task<IEnumerable<SubCategoriaEntity>> ListaSubCategoriasAsync();
	}
}

