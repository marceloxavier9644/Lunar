using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Condicional Devolução")]
    public class CondicionalDevolucao : ObjetoPadrao
    {
        private int id;
        private DateTime dataDevolucao;
        private double quantidade;
        private Condicional condicional;
        private Produto produto;
        private String observacoes;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data Devolução")]
        public virtual DateTime DataDevolucao { get => dataDevolucao; set => dataDevolucao = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Condicional Produto")]
        public virtual Condicional Condicional { get => condicional; set => condicional = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
    }
}
