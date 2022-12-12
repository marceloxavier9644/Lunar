using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Balanço de Estoque")]
    public class BalancoEstoque : ObjetoPadrao
    {
        private int id;
        private String descricao;
        private bool efetivado;
        private DateTime dataAjuste;
        private bool conciliado;
        private EmpresaFilial filial;
        private Usuario operadorEfetivacao;
        private bool zerarEstoqueNaoContado;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Efetivado")]
        public virtual bool Efetivado { get => efetivado; set => efetivado = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial Filial { get => filial; set => filial = value; }
        [Anotacao("Data Ajuste")]
        public virtual DateTime DataAjuste { get => dataAjuste; set => dataAjuste = value; }
        [Anotacao("Conciliado")]
        public virtual bool Conciliado { get => conciliado; set => conciliado = value; }
        [Anotacao("Operador Efetivação")]
        public virtual Usuario OperadorEfetivacao { get => operadorEfetivacao; set => operadorEfetivacao = value; }
        public virtual bool ZerarEstoqueNaoContado { get => zerarEstoqueNaoContado; set => zerarEstoqueNaoContado = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
