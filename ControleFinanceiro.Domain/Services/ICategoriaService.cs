using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Services
{
	public interface ICategoriaService
	{
        /// <summary>
        /// Verifica se uma Categoria existe.
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns>True ou False</returns>
        Task<bool> CategoriaExisteAsync(int idCategoria);

        /// <summary>
        /// retorna uma Categoria.
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        Task<CategoriaEntity> RetornaCategoriaAsync(int idCategoria);

        /// <summary>
        /// Inclui uma nova Categoria
        /// </summary>
        /// <param name="categoria">Objeto Categoria</param>
        //// <exception cref="ValidationCoreException"></exception>
        Task IncluiCategoriaAsync(CategoriaEntity categoria);        

        /// <summary>
        /// Atualiza uma Categoria
        /// </summary>
        /// <param name="categoria">Objeto Categoria</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task AtualizaCategoriaAsync(CategoriaEntity categoria);

        /// <summary>
        /// Exclui uma Categoria
        /// </summary>
        /// <param name="idCategoria">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.CategoriaCoreException"></exception>
        Task ExcluiCategoriaAsync(int idCategoria);

        /// <summary>
        /// Retorna uma lista de Categorias
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoriaEntity>> RetornaCategoriasAsync();
    }
}

