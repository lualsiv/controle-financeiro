using System;
using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
    public class SubCategoriaCoreError : CoreError
    {
        protected SubCategoriaCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly SubCategoriaCoreError SubCategoriaNaoEncontrado = new SubCategoriaCoreError("400.001", "Categoria não encontrada.");
        public static readonly SubCategoriaCoreError SubCategoriaExistente = new SubCategoriaCoreError("400.002", "Categoria já cadastrado.");
    }
}

