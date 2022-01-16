using System;
namespace ControleFinanceiro.WebApi.Dtos.Lancamento
{
	public class LancamentoInput
	{
        public int id { get; set; }

        public decimal valor { get; set; }

        public DateTime data { get; set; }

        public int idSubCategoria { get; set; }

        public string comentario { get; set; }

        public TipoLancamento tipoLancamento { get; set; }
    }

    public enum TipoLancamento
    {
        Creadito = 1,
        Debito = 2
    }
}

