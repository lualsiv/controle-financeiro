using System;
namespace ControleFinanceiro.Domain.Models
{
	public class SubCategoriaEntity
	{
        public int id { get; set; }

        public string nome { get; set; }

        public int idCategoria { get; set; }
    }
}

