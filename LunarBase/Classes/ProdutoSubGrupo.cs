using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("SubGrupo")]
    public class ProdutoSubGrupo : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private ProdutoGrupo produtoGrupo = new ProdutoGrupo();
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("SubGrupo")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Grupo")]
        public virtual ProdutoGrupo ProdutoGrupo { get => produtoGrupo; set => produtoGrupo = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
