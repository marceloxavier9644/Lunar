using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Tipo de Objeto")]
    public class TipoObjeto : ObjetoPadrao
    {
        private int id;
        private string descricao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
