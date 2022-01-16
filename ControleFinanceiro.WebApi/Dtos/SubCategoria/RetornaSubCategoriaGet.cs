using System;
namespace ControleFinanceiro.WebApi.Dtos.SubCategoria
{
	public class RetornaSubCategoriaGet
	{
		public int id { get; set; }

		public string nome { get; set; }

		public int idCategoria { get; set; }
	}
}

