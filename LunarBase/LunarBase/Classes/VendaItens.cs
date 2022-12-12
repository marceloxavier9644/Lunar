using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Venda Itens")]
    public class VendaItens : ObjetoPadrao
    {
        private int id;
        private Produto produto;
        private double quantidade;
        private string descricaoProduto;
        private string ncm;
        private string cest;
        private string unidadeMedida;
        private string cfop;
//ICMS
        private string origemIcms;
        private string cstIcms;
        private decimal baseIcms;
        private string aliqIcms;
        private decimal valorIcms;
        private decimal valorIsentoIcms;
        private decimal outrosIcms;
//IPI
        private string cstIpi;
        private decimal baseIpi;
        private string aliqIpi;
        private decimal valorIpi;
        private string codEnqIpi;
        private string codSeloIpi;
//PIS
        private string cstPis;
        private decimal basePis;
        private string aliqPis;
        private decimal valorPis;
        private decimal valorIsentoPis;
//COFINS
        private string cstCofins;
        private decimal baseCofins;
        private string aliqCofins;
        private decimal valorCofins;
        private decimal valorIsentoCofins;
//ANP
        private string codAnp;
        private double percGlp;
        private double percGnn;
        private double percGni;
        private decimal valorPartida;

        private decimal valorProduto;
        private decimal valorDesconto;
        private decimal valorAcrescimo;
        private decimal valorFinal;

        private Venda venda;
        private EmpresaFilial empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Descrição Produto")]
        public virtual string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }
        [Anotacao("NCM")]
        public virtual string Ncm { get => ncm; set => ncm = value; }
        [Anotacao("CEST")]
        public virtual string Cest { get => cest; set => cest = value; }
        [Anotacao("Unidade de Medida")]
        public virtual string UnidadeMedida { get => unidadeMedida; set => unidadeMedida = value; }
        [Anotacao("CFOP")]
        public virtual string Cfop { get => cfop; set => cfop = value; }
        [Anotacao("Origem ICMS")]
        public virtual string OrigemIcms { get => origemIcms; set => origemIcms = value; }
        [Anotacao("CST/CSOSN")]
        public virtual string CstIcms { get => cstIcms; set => cstIcms = value; }
        [Anotacao("Base ICMS")]
        public virtual decimal BaseIcms { get => baseIcms; set => baseIcms = value; }
        [Anotacao("Aliq. ICMS")]
        public virtual string AliqIcms { get => aliqIcms; set => aliqIcms = value; }
        [Anotacao("Valor ICMS")]
        public virtual decimal ValorIcms { get => valorIcms; set => valorIcms = value; }
        [Anotacao("Valor Isento ICMS")]
        public virtual decimal ValorIsentoIcms { get => valorIsentoIcms; set => valorIsentoIcms = value; }
        [Anotacao("Outros ICMS")]
        public virtual decimal OutrosIcms { get => outrosIcms; set => outrosIcms = value; }
        [Anotacao("Cst IPI")]
        public virtual string CstIpi { get => cstIpi; set => cstIpi = value; }
        [Anotacao("Base IPI")]
        public virtual decimal BaseIpi { get => baseIpi; set => baseIpi = value; }
        [Anotacao("Aliq. IPI")]
        public virtual string AliqIpi { get => aliqIpi; set => aliqIpi = value; }
        [Anotacao("Valor IPI")]
        public virtual decimal ValorIpi { get => valorIpi; set => valorIpi = value; }
        [Anotacao("Cod Enq IPI")]
        public virtual string CodEnqIpi { get => codEnqIpi; set => codEnqIpi = value; }
        [Anotacao("Selo IPI")]
        public virtual string CodSeloIpi { get => codSeloIpi; set => codSeloIpi = value; }
        [Anotacao("CST PIS")]
        public virtual string CstPis { get => cstPis; set => cstPis = value; }
        [Anotacao("Base PIS")]
        public virtual decimal BasePis { get => basePis; set => basePis = value; }
        [Anotacao("Aliq. PIS")]
        public virtual string AliqPis { get => aliqPis; set => aliqPis = value; }
        [Anotacao("Valor PIS")]
        public virtual decimal ValorPis { get => valorPis; set => valorPis = value; }
        [Anotacao("Valor Isento PIS")]
        public virtual decimal ValorIsentoPis { get => valorIsentoPis; set => valorIsentoPis = value; }
        [Anotacao("CST Cofins")]
        public virtual string CstCofins { get => cstCofins; set => cstCofins = value; }
        [Anotacao("Base Cofins")]
        public virtual decimal BaseCofins { get => baseCofins; set => baseCofins = value; }
        [Anotacao("Aliq. COFINS")]
        public virtual string AliqCofins { get => aliqCofins; set => aliqCofins = value; }
        [Anotacao("Valor COFINS")]
        public virtual decimal ValorCofins { get => valorCofins; set => valorCofins = value; }
        [Anotacao("Valor Isento COFINS")]
        public virtual decimal ValorIsentoCofins { get => valorIsentoCofins; set => valorIsentoCofins = value; }
        [Anotacao("COD. ANP")]
        public virtual string CodAnp { get => codAnp; set => codAnp = value; }
        [Anotacao("Perc. GLP")]
        public virtual double PercGlp { get => percGlp; set => percGlp = value; }
        [Anotacao("Perc. Gás Natural Nacional")]
        public virtual double PercGnn { get => percGnn; set => percGnn = value; }
        [Anotacao("Perc. Gás Natural Importado")]
        public virtual double PercGni { get => percGni; set => percGni = value; }
        [Anotacao("Valor Partida GLP")]
        public virtual decimal ValorPartida { get => valorPartida; set => valorPartida = value; }
        [Anotacao("Valor Produto")]
        public virtual decimal ValorProduto { get => valorProduto; set => valorProduto = value; }
        [Anotacao("Valor Desconto")]
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        [Anotacao("Valor Acréscimo")]
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }
        [Anotacao("Valor Final")]
        public virtual decimal ValorFinal { get => valorFinal; set => valorFinal = value; }
        [Anotacao("Venda ID")]
        public virtual Venda Venda { get => venda; set => venda = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }


        //Contrutor para conseguir puxar SUM na consulta DAO
        public VendaItens()
        {
        }

        public VendaItens(double quantidade, string descricaoProduto, Produto produto)
        {
            this.quantidade = quantidade;
            this.descricaoProduto = descricaoProduto;
            this.produto = produto;
        }
    }


}
