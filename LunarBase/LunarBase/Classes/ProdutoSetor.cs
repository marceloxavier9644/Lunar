using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Setor/Localização")]
    public class ProdutoSetor : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private EmpresaFilial empresaFilial;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Setor")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Filial")]
        [OcultarEmGridsEPesquisas]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
