using System;
using Otc.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Infra.Repository
{
	public class RepositoryConfiguration
	{
		[Required]
		public string SqlConnectionString { get; set; }
	}
}

