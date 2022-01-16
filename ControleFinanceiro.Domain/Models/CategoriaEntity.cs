using System;
using Otc.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Models
{
	public class CategoriaEntity
	{
        public int id { get; set; }

        [Required(ErrorKey = "400.001")]
        [MinLength(3, ErrorKey = "400.002")]
        public string nome { get; set; }
    }
}

