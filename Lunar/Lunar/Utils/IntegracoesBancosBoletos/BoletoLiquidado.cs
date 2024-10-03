using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.IntegracoesBancosBoletos
{
    public class BoletoLiquidado
    {
        public string Cooperativa { get; set; }
        public string CodigoBeneficiario { get; set; }
        public string CooperativaPostoBeneficiario { get; set; }
        public string NossoNumero { get; set; }
        public string SeuNumero { get; set; }
        public string TipoCarteira { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorLiquidado { get; set; }
        public decimal JurosLiquido { get; set; }
        public decimal DescontoLiquido { get; set; }
        public decimal MultaLiquida { get; set; }
        public decimal AbatimentoLiquido { get; set; }
        public string TipoLiquidacao { get; set; }
    }

    public class BoletoLiquidadoResponse
    {
        public List<BoletoLiquidado> Items { get; set; }
    }
}
