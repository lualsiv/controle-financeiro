using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Services
{
	public interface ISubCategoriaService
	{
        /// <summary>
        /// Verifica se uma SubCategoria existe.
        /// </summary>
        /// <param name="idSubCategoria"></param>
        /// <returns>True ou False</returns>
        Task<bool> SubCategoriaExisteAsync(int idSubCategoria);


        /// <summary>
        /// Retorna uma SubCategoria
        /// </summary>
        /// <param name="idSubCategoria"></param>
        /// <returns></returns>
        Task<SubCategoriaEntity> RetornaSubCategoriaAsync(int idSubCategoria);

        /// <summary>
        /// Inclui uma nova SubCategoria
        /// </summary>
        /// <param name="subCategoria">Objeto SubCategoria</param>
        //// <exception cref="ValidationCoreException"></exception>
        Task IncluiSubCategoriaAsync(SubCategoriaEntity subCategoria);        

        /// <summary>
        /// Atualiza uma SubCategoria
        /// </summary>
        /// <param name="categoria">Objeto Categoria</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task AtualizaSubCategoriaAsync(SubCategoriaEntity subCategoria);

        /// <summary>
        /// Exclui uma SubCategoria
        /// </summary>
        /// <param name="idSubCategoria">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.SubCategoriaCoreException"></exception>
        Task ExcluiSubCategoriaAsync(int idSubCategoria);

        /// <summary>
        /// Retorna uma lista de SubCategorias
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubCategoriaEntity>> RetornaSubCategoriasAsync();
    }
}

