using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Mesas/Comandas")]
    public class AtendimentoMesa : ObjetoPadrao
    {
        private int id;
        private string numero;
        private string tipo;
        private string status;


        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Numero")]
        public virtual string Numero { get => numero; set => numero = value; }
        [Anotacao("Tipo")]
        public virtual string Tipo { get => tipo; set => tipo = value; }
        [Anotacao("Status")]
        public virtual string Status { get => status; set => status = value; }
    }
}
