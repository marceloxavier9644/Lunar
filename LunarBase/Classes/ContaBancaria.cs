using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Conta Bancária")]
    public class ContaBancaria : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string agencia;
        private string dvAgencia;
        private string conta;
        private string dvConta;
        private string pix;
        private Banco banco;
        private EmpresaFilial empresaFilial;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Agência")]
        public virtual string Agencia { get => agencia; set => agencia = value; }
        [Anotacao("Dv Agência")]
        public virtual string DvAgencia { get => dvAgencia; set => dvAgencia = value; }
        [Anotacao("Conta")]
        public virtual string Conta { get => conta; set => conta = value; }
        [Anotacao("Dv Conta")]
        public virtual string DvConta { get => dvConta; set => dvConta = value; }
        [Anotacao("Banco")]
        public virtual Banco Banco { get => banco; set => banco = value; }
        [Anotacao("Empresa")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("PIX")]
        public virtual string Pix { get => pix; set => pix = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
