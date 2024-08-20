using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Grupo")]
    public class ProdutoGrupo : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private bool food;
        private string caminhoImagem;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Grupo")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }
        [Anotacao("Food?")]
        public virtual bool Food { get => food; set => food = value; }
        [Anotacao("Imagem")]
        [OcultarEmGridsEPesquisas]
        public virtual string CaminhoImagem { get => caminhoImagem; set => caminhoImagem = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
