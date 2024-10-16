using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class RetornoBanco : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string cooperativa;
        private string codigoBeneficiario;
        private string cooperativaPostoBeneficiario;
        private string nossoNumero;
        private string seuNumero;
        private string tipoCarteira;
        private DateTime dataPagamento;
        private decimal valor;
        private decimal valorLiquidado;
        private decimal jurosLiquido;
        private decimal descontoLiquido;
        private decimal multaLiquida;
        private decimal abatimentoLiquido;
        private string tipoLiquidacao;
        private ContaReceber contaReceber;

        public virtual int Id { get => id; set => id = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public virtual string Cooperativa { get => cooperativa; set => cooperativa = value; }
        public virtual string CodigoBeneficiario { get => codigoBeneficiario; set => codigoBeneficiario = value; }
        public virtual string CooperativaPostoBeneficiario { get => cooperativaPostoBeneficiario; set => cooperativaPostoBeneficiario = value; }
        public virtual string NossoNumero { get => nossoNumero; set => nossoNumero = value; }
        public virtual string SeuNumero { get => seuNumero; set => seuNumero = value; }
        public virtual string TipoCarteira { get => tipoCarteira; set => tipoCarteira = value; }
        public virtual DateTime DataPagamento { get => dataPagamento; set => dataPagamento = value; }
        public virtual decimal Valor { get => valor; set => valor = value; }
        public virtual decimal ValorLiquidado { get => valorLiquidado; set => valorLiquidado = value; }
        public virtual decimal JurosLiquido { get => jurosLiquido; set => jurosLiquido = value; }
        public virtual decimal DescontoLiquido { get => descontoLiquido; set => descontoLiquido = value; }
        public virtual decimal MultaLiquida { get => multaLiquida; set => multaLiquida = value; }
        public virtual decimal AbatimentoLiquido { get => abatimentoLiquido; set => abatimentoLiquido = value; }
        public virtual string TipoLiquidacao { get => tipoLiquidacao; set => tipoLiquidacao = value; }
        public virtual ContaReceber ContaReceber { get => contaReceber; set => contaReceber = value; }
    }
}
