using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Plano de Contas")]
    public class PlanoConta : ObjetoPadrao
    {
        private int id;
        private string idPai;
        private string idAcima;
        private string classificacao;
        private string descricao;
        private string tipo;
        private string tipoConta;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("ID Pai")]
        public virtual string IdPai { get => idPai; set => idPai = value; }
        [Anotacao("Classificação")]
        public virtual string Classificacao { get => classificacao; set => classificacao = value; }
        [Anotacao("Plano de Contas")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("ID Acima")]
        public virtual string IdAcima { get => idAcima; set => idAcima = value; }
        [Anotacao("Tipo Despesa")]
        public virtual string Tipo { get => tipo; set => tipo = value; }
        [Anotacao("Despesa/Receita")]
        public virtual string TipoConta { get => tipoConta; set => tipoConta = value; }

        public override string ToString()
        {
            return descricao;
        }

    }
}
