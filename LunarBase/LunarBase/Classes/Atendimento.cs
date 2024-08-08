using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Atendimento")]
    public class Atendimento : ObjetoPadrao
    {
        private int id;
        private string identificacao;
        private DateTime data;
        private string observacao;
        private int empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Identificacao")]
        public virtual string Identificacao { get => identificacao; set => identificacao = value; }
        [Anotacao("Data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Anotacao("Observações")]
        public virtual string Observacao { get => observacao; set => observacao = value; }
        [Anotacao("Filial")]
        public virtual int EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }

    }
}
