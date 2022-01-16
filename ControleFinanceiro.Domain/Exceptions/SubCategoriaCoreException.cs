using System;
using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
	public class SubCategoriaCoreException : CoreException<SubCategoriaCoreError>
	{
		public SubCategoriaCoreException() : base()
		{
		}

		public SubCategoriaCoreException(params SubCategoriaCoreError[] errors) => AddError(errors);

		public override string Key => "SubCategoriaCoreException";
	}
}

