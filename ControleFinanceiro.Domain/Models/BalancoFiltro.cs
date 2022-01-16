using System;
namespace ControleFinanceiro.Domain.Models
{
	public class BalancoFiltro
	{
        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
        public int? idCategoria { get; set; }
    }
}

