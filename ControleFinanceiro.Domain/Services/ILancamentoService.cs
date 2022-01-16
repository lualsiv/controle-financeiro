using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Services
{
    public interface ILancamentoService
	{

        /// <summary>
        /// Retorna um Lancamento
        /// </summary>
        /// <param name="idLancamento"></param>
        /// <returns></returns>
        Task<LancamentoEntity> RetornaLancamentoAsync(int idLancamento);

        /// <summary>
        /// Inclui um novo Lancamento
        /// </summary>
        /// <param name="lancamento">Objeto Lancamento</param>
        //// <exception cref="ValidationCoreException"></exception>
        Task IncluiLancamentoAsync(LancamentoEntity lancamento);

        Task<bool> LancamentoExisteAsync(int idLancamento);

        /// <summary>
        /// Atualiza um Lancamento
        /// </summary>
        /// <param name="categoria">Objeto Categoria</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task AtualizaLancamentoAsync(LancamentoEntity lancamento);

        /// <summary>
        /// Exclui um Lancamento
        /// </summary>
        /// <param name="idLancamento">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.LancamentoCoreException"></exception>
        Task ExcluiLancamentoAsync(int idLancamento);

        /// <summary>
        /// Retorna uma lista de Lancamentos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LancamentoEntity>> RetornaLancamentosAsync();
    }
}

