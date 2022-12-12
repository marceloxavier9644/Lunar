using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Produto")]
    public class Produto : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string ean;
        private decimal valorVenda;
        private decimal valorCusto;
        private string ncm;
        private string cest;
        private bool controlaEstoque;
        private bool solicitaNumeroSerie;
        private bool ecommerce;
        private bool grade;
        private bool pesavel;
        private GrupoFiscal grupoFiscal;
        private Marca marca;
        private ProdutoGrupo produtoGrupo;
        private ProdutoSubGrupo produtoSubGrupo;
        private Empresa empresa;
        private EmpresaFilial empresaFilial;
        private UnidadeMedida unidadeMedida;
        private ProdutoSetor produtoSetor;
        private double estoque;
        private double estoqueAuxiliar;
        private string referencia;
        private string tipoProduto;
        private string observacoes;

        private string cstIcms;
        private string percentualIcms;
        private string cfopVenda;
        private string cstIpi;
        private string percentualIpi;
        private string enqIpi;
        private string codSeloIpi;
        private string cstPis;
        private string percentualPis;
        private string cstCofins;
        private string percentualCofins;
        private string codAnp;
        private double percGlp;
        private double percGnn;
        private double percGni;
        private decimal valorPartida;
        private string origemIcms;
        private string idComplementar;
           

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição do Produto")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Código de Barras")]
        public virtual string Ean { get => ean; set => ean = value; }
        [Anotacao("Valor de Venda")]
        public virtual decimal ValorVenda { get => valorVenda; set => valorVenda = value; }
        [Anotacao("Valor de Custo")]
        public virtual decimal ValorCusto { get => valorCusto; set => valorCusto = value; }
        [Anotacao("NCM")]
        public virtual string Ncm { get => ncm; set => ncm = value; }
        [Anotacao("CEST")]
        public virtual string Cest { get => cest; set => cest = value; }
        [Anotacao("Controla Estoque?")]
        public virtual bool ControlaEstoque { get => controlaEstoque; set => controlaEstoque = value; }
        [Anotacao("Grade")]
        public virtual bool Grade { get => grade; set => grade = value; }
        [Anotacao("Solicita Nº Série?")]
        public virtual bool SolicitaNumeroSerie { get => solicitaNumeroSerie; set => solicitaNumeroSerie = value; }
        [Anotacao("Ecommerce")]
        public virtual bool Ecommerce { get => ecommerce; set => ecommerce = value; }
        [Anotacao("Grupo Fiscal")]
        public virtual GrupoFiscal GrupoFiscal { get => grupoFiscal; set => grupoFiscal = value; }
        [Anotacao("Marca")]
        public virtual Marca Marca { get => marca; set => marca = value; }
        [Anotacao("Grupo")]
        public virtual ProdutoGrupo ProdutoGrupo { get => produtoGrupo; set => produtoGrupo = value; }
        [Anotacao("SubGrupo")]
        public virtual ProdutoSubGrupo ProdutoSubGrupo { get => produtoSubGrupo; set => produtoSubGrupo = value; }
        [Anotacao("Empresa")]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Estoque")]
        public virtual double Estoque { get => estoque; set => estoque = value; }
        [Anotacao("Estoque Ajustado")]
        public virtual double EstoqueAuxiliar { get => estoqueAuxiliar; set => estoqueAuxiliar = value; }
        [Anotacao("Referência")]
        public virtual string Referencia { get => referencia; set => referencia = value; }
        [Anotacao("Unidade de Medida")]
        public virtual UnidadeMedida UnidadeMedida { get => unidadeMedida; set => unidadeMedida = value; }
        [Anotacao("Setor")]
        public virtual ProdutoSetor ProdutoSetor { get => produtoSetor; set => produtoSetor = value; }
        [Anotacao("Tipo de Produto")]
        public virtual string TipoProduto { get => tipoProduto; set => tipoProduto = value; }
        [Anotacao("Observacoes")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Pesavel")]
        public virtual bool Pesavel { get => pesavel; set => pesavel = value; }
        [Anotacao("Cst ICMS")]
        public virtual string CstIcms { get => cstIcms; set => cstIcms = value; }
        [Anotacao("Percentual ICMS")]
        public virtual string PercentualIcms { get => percentualIcms; set => percentualIcms = value; }
        [Anotacao("CFOP Venda")]
        public virtual string CfopVenda { get => cfopVenda; set => cfopVenda = value; }
        [Anotacao("CST IPI")]
        public virtual string CstIpi { get => cstIpi; set => cstIpi = value; }
        [Anotacao("Percentual IPI")]
        public virtual string PercentualIpi { get => percentualIpi; set => percentualIpi = value; }
        [Anotacao("Enq. IPI")]
        public virtual string EnqIpi { get => enqIpi; set => enqIpi = value; }
        [Anotacao("Cód. Selo IPI")]
        public virtual string CodSeloIpi { get => codSeloIpi; set => codSeloIpi = value; }
        [Anotacao("CST PIS")]
        public virtual string CstPis { get => cstPis; set => cstPis = value; }
        [Anotacao("Percentual PIS")]
        public virtual string PercentualPis { get => percentualPis; set => percentualPis = value; }
        [Anotacao("CST Cofins")]
        public virtual string CstCofins { get => cstCofins; set => cstCofins = value; }
        [Anotacao("Percentual Cofins")]
        public virtual string PercentualCofins { get => percentualCofins; set => percentualCofins = value; }
        [Anotacao("Cód. ANP")]
        public virtual string CodAnp { get => codAnp; set => codAnp = value; }
        [Anotacao("Origem")]
        public virtual string OrigemIcms { get => origemIcms; set => origemIcms = value; }
        [Anotacao("Percentual GLP")]
        [OcultarEmGridsEPesquisas]
        public virtual double PercGlp { get => percGlp; set => percGlp = value; }
        [Anotacao("Percentual Gás Natural Nacional")]
        [OcultarEmGridsEPesquisas]
        public virtual double PercGnn { get => percGnn; set => percGnn = value; }
        [Anotacao("Percentual Gás Natural Importado")]
        [OcultarEmGridsEPesquisas]
        public virtual double PercGni { get => percGni; set => percGni = value; }
        [Anotacao("Valor Partida")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal ValorPartida { get => valorPartida; set => valorPartida = value; }
        [Anotacao("ID Complementar")]
        public virtual string IdComplementar { get => idComplementar; set => idComplementar = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
