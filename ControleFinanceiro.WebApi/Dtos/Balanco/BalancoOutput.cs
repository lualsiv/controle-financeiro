using System;
using ControleFinanceiro.WebApi.Dtos.Categoria;

namespace ControleFinanceiro.WebApi.Dtos.Balanco
{
	public class BalancoOutput
	{
		public RetornaCategoriaGet categoria { get; set; }
		public double receita { get; set; }
		public double despesa { get; set; }
		public double saldo { get; set; }
	}
}

