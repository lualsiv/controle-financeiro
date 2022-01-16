using System;
namespace ControleFinanceiro.WebApi.Dtos.Balanco
{
	public class BalancoInput
	{
		public DateTime? dataInicio { get; set; }
		public DateTime? dataFim { get; set; }
		public int? idCategoria { get; set; }
	}
}

