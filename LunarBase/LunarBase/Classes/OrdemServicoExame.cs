using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Ordem de Servico Exame")]
    public class OrdemServicoExame : ObjetoPadrao
    {
        private int id;
        private string examinador;
        private DateTime dataExame;
        //Longe Direito
        private string ldEsferico;
        private string ldCilindrico;
        private string ldPosicao;
        private string ldDp;
        private string ldAltura;
        //Longe Esquerdo
        private string leEsferico;
        private string leCilindrico;
        private string lePosicao;
        private string leDp;
        private string leAltura;
        //Perto Direito
        private string pdEsferico;
        private string pdCilindrico;
        private string pdPosicao;
        private string pdDp;
        private string pdAltura;
        //Perto Esquerdo
        private string peEsferico;
        private string peCilindrico;
        private string pePosicao;
        private string peDp;
        private string peAltura;

        private string armacao;
        private string lente;
        private string proximoExame;
        private string adicao;
        private DateTime dataEntrega;

        private string observacoes;
        private OrdemServico ordemServico;
        private Pessoa pessoa;
        private PessoaDependente dependente;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Examinador")]
        public virtual string Examinador { get => examinador; set => examinador = value; }
        [Anotacao("Data Exame")]
        public virtual DateTime DataExame { get => dataExame; set => dataExame = value; }
        [Anotacao("LD Esférico")]
        public virtual string LdEsferico { get => ldEsferico; set => ldEsferico = value; }
        [Anotacao("LD Cilindrico")]
        public virtual string LdCilindrico { get => ldCilindrico; set => ldCilindrico = value; }
        [Anotacao("LD Posição Eixo")]
        public virtual string LdPosicao { get => ldPosicao; set => ldPosicao = value; }
        [Anotacao("LD DP")]
        public virtual string LdDp { get => ldDp; set => ldDp = value; }
        [Anotacao("LD Altura")]
        public virtual string LdAltura { get => ldAltura; set => ldAltura = value; }
        [Anotacao("LE Esferico")]
        public virtual string LeEsferico { get => leEsferico; set => leEsferico = value; }
        [Anotacao("LE Cilindrico")]
        public virtual string LeCilindrico { get => leCilindrico; set => leCilindrico = value; }
        [Anotacao("Le Posicao")]
        public virtual string LePosicao { get => lePosicao; set => lePosicao = value; }
        [Anotacao("LE DP")]
        public virtual string LeDp { get => leDp; set => leDp = value; }
        [Anotacao("LE Altura")]
        public virtual string LeAltura { get => leAltura; set => leAltura = value; }
        [Anotacao("PD Esferico")]
        public virtual string PdEsferico { get => pdEsferico; set => pdEsferico = value; }
        [Anotacao("PD Cilindrico")]
        public virtual string PdCilindrico { get => pdCilindrico; set => pdCilindrico = value; }
        [Anotacao("PD Posicao")]
        public virtual string PdPosicao { get => pdPosicao; set => pdPosicao = value; }
        [Anotacao("PD DP")]
        public virtual string PdDp { get => pdDp; set => pdDp = value; }
        [Anotacao("PD Altura")]
        public virtual string PdAltura { get => pdAltura; set => pdAltura = value; }
        [Anotacao("PD Esférico")]
        public virtual string PeEsferico { get => peEsferico; set => peEsferico = value; }
        [Anotacao("Pe Cilindrico")]
        public virtual string PeCilindrico { get => peCilindrico; set => peCilindrico = value; }
        [Anotacao("Pe Posicao")]
        public virtual string PePosicao { get => pePosicao; set => pePosicao = value; }
        [Anotacao("Pe DP")]
        public virtual string PeDp { get => peDp; set => peDp = value; }
        [Anotacao("Pe Altura")]
        public virtual string PeAltura { get => peAltura; set => peAltura = value; }
        [Anotacao("Armação")]
        public virtual string Armacao { get => armacao; set => armacao = value; }
        [Anotacao("Lente")]
        public virtual string Lente { get => lente; set => lente = value; }
        [Anotacao("Proximo Exame")]
        public virtual string ProximoExame { get => proximoExame; set => proximoExame = value; }
        [Anotacao("Adição")]
        public virtual string Adicao { get => adicao; set => adicao = value; }
        [Anotacao("Data Entrega")]
        public virtual DateTime DataEntrega { get => dataEntrega; set => dataEntrega = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Ordem de Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Dependente")]
        public virtual PessoaDependente Dependente { get => dependente; set => dependente = value; }

        public override string ToString()
        {
            return examinador;
        }
    }
}
