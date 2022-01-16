using System;
using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
	public class LancamentoCoreException : CoreException<LancamentoCoreError>
	{
		public LancamentoCoreException() : base()
		{
		}

		public LancamentoCoreException(params LancamentoCoreError[] errors) => AddError(errors);

		public override string Key => "LancamentoCoreException";
	}
}

