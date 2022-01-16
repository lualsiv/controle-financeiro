using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
	public class CategoriaCoreError : CoreError
	{
        protected CategoriaCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly CategoriaCoreError CategoriaNaoEncontrado = new CategoriaCoreError("400.001", "Categoria não encontrada.");
        public static readonly CategoriaCoreError CategoriaExistente = new CategoriaCoreError("400.002", "Categoria já cadastrado.");
    }
}

