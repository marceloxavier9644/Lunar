using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("NFE Status")]
    public class NfeStatus : ObjetoPadrao
    {
        private int id;
        private string codStatus;
        private string status;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Código Status")]
        public virtual string CodStatus { get => codStatus; set => codStatus = value; }
        [Anotacao("Status")]
        public virtual string Status { get => status; set => status = value; }

        public override string ToString()
        {
            return status;
        }
    }
}
