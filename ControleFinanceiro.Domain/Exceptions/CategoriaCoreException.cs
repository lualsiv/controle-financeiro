using System;
using ControleFinanceiro.DomainBase.Exceptions;

namespace ControleFinanceiro.Domain.Exceptions
{
	public class CategoriaCoreException : CoreException<CategoriaCoreError>
	{
		public CategoriaCoreException() : base()	
		{
		}

		public CategoriaCoreException(params CategoriaCoreError[] errors) => AddError(errors);

		public override string Key => "CategoriaCoreException";
    }
}

