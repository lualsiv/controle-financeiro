using System;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
	public interface ISubCategoriaWriteOnlyRepository
	{
		Task IncluiSubCategoriaAsync(SubCategoriaEntity subCategoria);

		Task AtualizaSubCategoriaAsync(SubCategoriaEntity subCategoria);

		Task ExcluiSubCategoriaAsync(int idSubCategoria);
	}
}

