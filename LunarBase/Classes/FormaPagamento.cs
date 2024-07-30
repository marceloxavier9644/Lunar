using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Forma de Pagamento")]
    public class FormaPagamento : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private bool caixa;
        private bool cheque;
        private bool cartao;
        private bool boleto;
        private bool crediario;
        private bool creditoCliente;
        private bool banco;
        private string codigoSefaz;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Caixa")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Caixa { get => caixa; set => caixa = value; }
        [Anotacao("Cheque")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Cheque { get => cheque; set => cheque = value; }
        [Anotacao("Cartão")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Cartao { get => cartao; set => cartao = value; }
        [Anotacao("Boleto")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Boleto { get => boleto; set => boleto = value; }
        [Anotacao("Crediário")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Crediario { get => crediario; set => crediario = value; }
        [Anotacao("Credito de Cliente")]
        [OcultarEmGridsEPesquisas]
        public virtual bool CreditoCliente { get => creditoCliente; set => creditoCliente = value; }
        [Anotacao("Banco")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Banco { get => banco; set => banco = value; }
        [Anotacao("Código Sefaz")]
        [OcultarEmGridsEPesquisas]
        public virtual string CodigoSefaz { get => codigoSefaz; set => codigoSefaz = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
