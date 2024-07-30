using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Troco Fixo")]
    public class TrocoFixo : ObjetoPadrao
    {
        private int id;
        private decimal valor;
        private Usuario usuario;
        private EmpresaFilial empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Valor")]
        public virtual decimal Valor { get => valor; set => valor = value; }
        [Anotacao("Usuario")]
        public virtual Usuario Usuario { get => usuario; set => usuario = value; }
        [Anotacao("Empresa Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
    }
}
