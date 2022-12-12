using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Parcelamento")]
    public class Parcelamento : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private double taxa;
        private int diasRecebimento;
        private int parcelas;
        private decimal tarifa;
        private double taxaAntecipacao;
        private bool credito;
        private bool debito;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Parcelamento")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Taxa")]
        public virtual double Taxa { get => taxa; set => taxa = value; }
        [Anotacao("Dias Recebimento")]
        public virtual int DiasRecebimento { get => diasRecebimento; set => diasRecebimento = value; }
        [Anotacao("Parcelas")]
        public virtual int Parcelas { get => parcelas; set => parcelas = value; }
        [Anotacao("Tarifa")]
        public virtual decimal Tarifa { get => tarifa; set => tarifa = value; }
        [Anotacao("Taxa Antecipação")]
        public virtual double TaxaAntecipacao { get => taxaAntecipacao; set => taxaAntecipacao = value; }
        [Anotacao("Crédito")]
        public virtual bool Credito { get => credito; set => credito = value; }
        [Anotacao("Débito")]
        public virtual bool Debito { get => debito; set => debito = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
