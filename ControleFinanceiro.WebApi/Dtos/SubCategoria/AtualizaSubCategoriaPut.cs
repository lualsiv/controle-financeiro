using System;
namespace ControleFinanceiro.WebApi.Dtos.SubCategoria
{
	public class AtualizaSubCategoriaPut
	{
        public int id { get; set; }

        public string nome { get; set; }

        public int idCategoria { get; set; }
    }
}

