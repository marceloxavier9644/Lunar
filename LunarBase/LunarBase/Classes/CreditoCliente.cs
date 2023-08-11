using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Crédito Cliente")]
    public class CreditoCliente : ObjetoPadrao
    {
        private int id;
        private string origem;
        private decimal valor;
        private decimal valorUtilizado;
        private DateTime dataUtilizacao;
        private Pessoa cliente;
        private EmpresaFilial empresaFilial;
        private string idOrigem;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Origem")]
        public virtual string Origem { get => origem; set => origem = value; }
        [Anotacao("Valor")]
        public virtual decimal Valor { get => valor; set => valor = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Valor Utilizado")]
        public virtual decimal ValorUtilizado { get => valorUtilizado; set => valorUtilizado = value; }
        [Anotacao("Data Utilização")]
        public virtual DateTime DataUtilizacao { get => dataUtilizacao; set => dataUtilizacao = value; }
        [Anotacao("ID Origem")]
        public virtual string IdOrigem { get => idOrigem; set => idOrigem = value; }

        public override string ToString()
        {
            return origem;
        }
    }
}
