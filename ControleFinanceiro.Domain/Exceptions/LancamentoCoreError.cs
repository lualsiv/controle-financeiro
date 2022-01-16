using System;
using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
	public class LancamentoCoreError : CoreError
    {
        protected LancamentoCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly LancamentoCoreError LancamentoNaoEncontrado = new LancamentoCoreError("400.001", "Lançamento não encontrado.");
        public static readonly LancamentoCoreError LancamentoExistente = new LancamentoCoreError("400.002", "Lançamento já cadastrado.");
    }
}

