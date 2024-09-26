using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("NFS-e")]
    public class Nfse : ObjetoPadrao
    {
        private int id;
        private string referencia;
        private string numeroRps;
        private string serieRps;
        private string status;
        private int numero;
        private string codigoVerificacao;
        private DateTime dataEmissao;
        private string url;
        private string caminhoXml;
        private string urlDanfe;
        private OrdemServico ordemServico;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Referencia")]
        public virtual string Referencia { get => referencia; set => referencia = value; }
        [Anotacao("RPS")]
        public virtual string NumeroRps { get => numeroRps; set => numeroRps = value; }
        [Anotacao("Serie")]
        public virtual string SerieRps { get => serieRps; set => serieRps = value; }
        [Anotacao("Status")]
        public virtual string Status { get => status; set => status = value; }
        [Anotacao("Numero")]
        public virtual int Numero { get => numero; set => numero = value; }
        [Anotacao("Codigo Verificacao")]
        public virtual string CodigoVerificacao { get => codigoVerificacao; set => codigoVerificacao = value; }
        [Anotacao("Data Emissão")]
        public virtual DateTime DataEmissao { get => dataEmissao; set => dataEmissao = value; }
        [Anotacao("URL")]
        public virtual string Url { get => url; set => url = value; }
        [Anotacao("Caminho XML")]
        public virtual string CaminhoXml { get => caminhoXml; set => caminhoXml = value; }
        [Anotacao("URL Danfe")]
        public virtual string UrlDanfe { get => urlDanfe; set => urlDanfe = value; }
        [Anotacao("Ordem de Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
    }
}
