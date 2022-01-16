using System;
namespace ControleFinanceiro.Domain.Models
{
	public class Balanco
	{
        public CategoriaEntity categoria { get; set; }
        public double receita { get; set; }
        public double despesa { get; set; }
        public double saldo { get => (receita - despesa); }
    }
}

