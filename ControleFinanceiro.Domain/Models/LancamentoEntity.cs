using System;
namespace ControleFinanceiro.Domain.Models
{
	public class LancamentoEntity
	{
        public int id { get; set; }

        public double valor { get; set; }

        public DateTime data { get; set; }

        public int idSubCategoria { get; set; }

        public string comentario { get; set; }

        public TipoLancamento tipoLancamento { get; set; }
    }

    public enum TipoLancamento
    {
        Credito = 1,
        Debito = 2
    }
}

