using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Taxa Cartão")]
    public class TaxaCartao : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private AdquirenteCartao adquirenteCartao;
        private BandeiraCartao bandeiraCartao;
        private int parcelas;
        private double taxa;
        private double taxaAdicionalPorParcela;
        private decimal tarifaAdicional;
        private int diasRecebimento;
        private Boolean antecipacao;
        private int diasRecebimentoAntecipacao;
        private double taxaAntecipacao;
        private string tipoCartao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Maquininha")]
        public virtual AdquirenteCartao AdquirenteCartao { get => adquirenteCartao; set => adquirenteCartao = value; }
        [Anotacao("Descrição")]
        public virtual BandeiraCartao BandeiraCartao { get => bandeiraCartao; set => bandeiraCartao = value; }
        [Anotacao("Parcelas")]
        public virtual int Parcelas { get => parcelas; set => parcelas = value; }
        [Anotacao("Taxa")]
        public virtual double Taxa { get => taxa; set => taxa = value; }
        [Anotacao("Tarifa Adicional")]
        public virtual decimal TarifaAdicional { get => tarifaAdicional; set => tarifaAdicional = value; }
        [Anotacao("Dias Recebimento")]
        public virtual int DiasRecebimento { get => diasRecebimento; set => diasRecebimento = value; }
        [Anotacao("Antecipacao")]
        public virtual bool Antecipacao { get => antecipacao; set => antecipacao = value; }
        [Anotacao("Dias Recebimento Antecipacao")]
        public virtual int DiasRecebimentoAntecipacao { get => diasRecebimentoAntecipacao; set => diasRecebimentoAntecipacao = value; }
        [Anotacao("taxa Antecipação")]
        public virtual double TaxaAntecipacao { get => taxaAntecipacao; set => taxaAntecipacao = value; }
        [Anotacao("Tipo Cartão")]
        public virtual string TipoCartao { get => tipoCartao; set => tipoCartao = value; }
        [Anotacao("Taxa Adicional Por Parcela")]
        public virtual double TaxaAdicionalPorParcela { get => taxaAdicionalPorParcela; set => taxaAdicionalPorParcela = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
