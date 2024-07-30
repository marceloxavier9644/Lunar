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

        private bool veiculo;
        private string corMontadora;
        private string corDenatran;
        private string tipoPintura;
        private string potenciaCv;
        private string cilindradaCc;
        private string numeroMotor;
        private string combustivel;
        private string tipoCambio;
        private string tipoEntrada;
        private string anoVeiculo;
        private string modeloVeiculo;
        private string marcaModelo;
        private string especieVeiculo;
        private string lotacaoVeiculo;
        private string tipoVeiculo;
        private string placa;
        private string renavam;
        private string chassi;
        private string condicaoVeiculo;
        private string distanciaEixo;
        private string capacidadeTracao;
        private string pesoLiquidoVeiculo;
        private string pesoBrutoVeiculo;
        private string condicaoProduto;
        private string restricaoVeiculo;
        private string kmEntrada;
        private bool veiculoNovo;
        private string markup;

        private ProdutoGrade gradePrincipal;


        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição do Produto")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Código de Barras")]
        [OcultarEmGridsEPesquisas]
        public virtual string Ean { get => ean; set => ean = value; }
        [Anotacao("Valor de Venda")]
        public virtual decimal ValorVenda { get => valorVenda; set => valorVenda = value; }
        [Anotacao("Valor de Custo")]
        public virtual decimal ValorCusto { get => valorCusto; set => valorCusto = value; }
        [Anotacao("NCM")]
        public virtual string Ncm { get => ncm; set => ncm = value; }
        [Anotacao("CEST")]
        [OcultarEmGridsEPesquisas]
        public virtual string Cest { get => cest; set => cest = value; }
        [Anotacao("Controla Estoque?")]
        [OcultarEmGridsEPesquisas]
        public virtual bool ControlaEstoque { get => controlaEstoque; set => controlaEstoque = value; }
        [Anotacao("Grade")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Grade { get => grade; set => grade = value; }
        [Anotacao("Solicita Nº Série?")]
        [OcultarEmGridsEPesquisas]
        public virtual bool SolicitaNumeroSerie { get => solicitaNumeroSerie; set => solicitaNumeroSerie = value; }
        [Anotacao("Ecommerce")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Ecommerce { get => ecommerce; set => ecommerce = value; }
        [Anotacao("Grupo Fiscal")]
        [OcultarEmGridsEPesquisas]
        public virtual GrupoFiscal GrupoFiscal { get => grupoFiscal; set => grupoFiscal = value; }
        [Anotacao("Marca")]
        [OcultarEmGridsEPesquisas]
        public virtual Marca Marca { get => marca; set => marca = value; }
        [Anotacao("Grupo")]
        [OcultarEmGridsEPesquisas]
        public virtual ProdutoGrupo ProdutoGrupo { get => produtoGrupo; set => produtoGrupo = value; }
        [Anotacao("SubGrupo")]
        [OcultarEmGridsEPesquisas]
        public virtual ProdutoSubGrupo ProdutoSubGrupo { get => produtoSubGrupo; set => produtoSubGrupo = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }
        [Anotacao("Filial")]
        [OcultarEmGridsEPesquisas]
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
        [OcultarEmGridsEPesquisas]
        public virtual string TipoProduto { get => tipoProduto; set => tipoProduto = value; }
        [Anotacao("Observacoes")]
        [OcultarEmGridsEPesquisas]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Pesavel")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Pesavel { get => pesavel; set => pesavel = value; }
        [Anotacao("Cst ICMS")]
        [OcultarEmGridsEPesquisas]
        public virtual string CstIcms { get => cstIcms; set => cstIcms = value; }
        [Anotacao("Percentual ICMS")]
        [OcultarEmGridsEPesquisas]
        public virtual string PercentualIcms { get => percentualIcms; set => percentualIcms = value; }
        [Anotacao("CFOP Venda")]
        public virtual string CfopVenda { get => cfopVenda; set => cfopVenda = value; }
        [Anotacao("CST IPI")]
        [OcultarEmGridsEPesquisas]
        public virtual string CstIpi { get => cstIpi; set => cstIpi = value; }
        [Anotacao("Percentual IPI")]
        [OcultarEmGridsEPesquisas]
        public virtual string PercentualIpi { get => percentualIpi; set => percentualIpi = value; }
        [Anotacao("Enq. IPI")]
        [OcultarEmGridsEPesquisas]
        public virtual string EnqIpi { get => enqIpi; set => enqIpi = value; }
        [Anotacao("Cód. Selo IPI")]
        [OcultarEmGridsEPesquisas]
        public virtual string CodSeloIpi { get => codSeloIpi; set => codSeloIpi = value; }
        [Anotacao("CST PIS")]
        [OcultarEmGridsEPesquisas]
        public virtual string CstPis { get => cstPis; set => cstPis = value; }
        [Anotacao("Percentual PIS")]
        [OcultarEmGridsEPesquisas]
        public virtual string PercentualPis { get => percentualPis; set => percentualPis = value; }
        [Anotacao("CST Cofins")]
        [OcultarEmGridsEPesquisas]
        public virtual string CstCofins { get => cstCofins; set => cstCofins = value; }
        [Anotacao("Percentual Cofins")]
        [OcultarEmGridsEPesquisas]
        public virtual string PercentualCofins { get => percentualCofins; set => percentualCofins = value; }
        [Anotacao("Cód. ANP")]
        public virtual string CodAnp { get => codAnp; set => codAnp = value; }
        [Anotacao("Origem")]
        [OcultarEmGridsEPesquisas]
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
        [Anotacao("Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Veiculo { get => veiculo; set => veiculo = value; }
        [Anotacao("Cor Montadora")]
        [OcultarEmGridsEPesquisas]
        public virtual string CorMontadora { get => corMontadora; set => corMontadora = value; }
        [Anotacao("Cor Denatran")]
        [OcultarEmGridsEPesquisas]
        public virtual string CorDenatran { get => corDenatran; set => corDenatran = value; }
        [Anotacao("Tipo Pintura")]
        [OcultarEmGridsEPesquisas]
        public virtual string TipoPintura { get => tipoPintura; set => tipoPintura = value; }
        [Anotacao("Potencia Cv")]
        [OcultarEmGridsEPesquisas]
        public virtual string PotenciaCv { get => potenciaCv; set => potenciaCv = value; }
        [Anotacao("Cilindrada CC")]
        [OcultarEmGridsEPesquisas]
        public virtual string CilindradaCc { get => cilindradaCc; set => cilindradaCc = value; }
        [Anotacao("Motor")]
        [OcultarEmGridsEPesquisas]
        public virtual string NumeroMotor { get => numeroMotor; set => numeroMotor = value; }
        [Anotacao("Combustivel")]
        [OcultarEmGridsEPesquisas]
        public virtual string Combustivel { get => combustivel; set => combustivel = value; }
        [Anotacao("Tipo Cambio")]
        [OcultarEmGridsEPesquisas]
        public virtual string TipoCambio { get => tipoCambio; set => tipoCambio = value; }
        [Anotacao("Tipo Entrada")]
        [OcultarEmGridsEPesquisas]
        public virtual string TipoEntrada { get => tipoEntrada; set => tipoEntrada = value; }
        [Anotacao("Ano Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string AnoVeiculo { get => anoVeiculo; set => anoVeiculo = value; }
        [Anotacao("Modelo")]
        [OcultarEmGridsEPesquisas]
        public virtual string ModeloVeiculo { get => modeloVeiculo; set => modeloVeiculo = value; }
        [Anotacao("Marca Modelo")]
        [OcultarEmGridsEPesquisas]
        public virtual string MarcaModelo { get => marcaModelo; set => marcaModelo = value; }
        [Anotacao("Especie")]
        [OcultarEmGridsEPesquisas]
        public virtual string EspecieVeiculo { get => especieVeiculo; set => especieVeiculo = value; }
        [Anotacao("Lotacao")]
        [OcultarEmGridsEPesquisas]
        public virtual string LotacaoVeiculo { get => lotacaoVeiculo; set => lotacaoVeiculo = value; }
        [Anotacao("Tipo Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string TipoVeiculo { get => tipoVeiculo; set => tipoVeiculo = value; }
        [Anotacao("Placa")]
        [OcultarEmGridsEPesquisas]
        public virtual string Placa { get => placa; set => placa = value; }
        [Anotacao("Renavam")]
        [OcultarEmGridsEPesquisas]
        public virtual string Renavam { get => renavam; set => renavam = value; }
        [Anotacao("Chassi")]
        [OcultarEmGridsEPesquisas]
        public virtual string Chassi { get => chassi; set => chassi = value; }
        [Anotacao("Condicao Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string CondicaoVeiculo { get => condicaoVeiculo; set => condicaoVeiculo = value; }
        [Anotacao("Distancia Eixo")]
        [OcultarEmGridsEPesquisas]
        public virtual string DistanciaEixo { get => distanciaEixo; set => distanciaEixo = value; }
        [Anotacao("Capacidade Tração")]
        [OcultarEmGridsEPesquisas]
        public virtual string CapacidadeTracao { get => capacidadeTracao; set => capacidadeTracao = value; }
        [Anotacao("Peso Liquido Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string PesoLiquidoVeiculo { get => pesoLiquidoVeiculo; set => pesoLiquidoVeiculo = value; }
        [Anotacao("Peso Bruto Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string PesoBrutoVeiculo { get => pesoBrutoVeiculo; set => pesoBrutoVeiculo = value; }
        [Anotacao("Condicao Produto")]
        [OcultarEmGridsEPesquisas]
        public virtual string CondicaoProduto { get => condicaoProduto; set => condicaoProduto = value; }
        [Anotacao("Restrição Veiculo")]
        [OcultarEmGridsEPesquisas]
        public virtual string RestricaoVeiculo { get => restricaoVeiculo; set => restricaoVeiculo = value; }
        [Anotacao("KM Entrada")]
        [OcultarEmGridsEPesquisas]
        public virtual string KmEntrada { get => kmEntrada; set => kmEntrada = value; }
        [Anotacao("Veiculo Novo")]
        [OcultarEmGridsEPesquisas]
        public virtual bool VeiculoNovo { get => veiculoNovo; set => veiculoNovo = value; }
        [Anotacao("Markup")]
        [OcultarEmGridsEPesquisas]
        public virtual string Markup { get => markup; set => markup = value; }
        [Anotacao("Grade")]
        [OcultarEmGridsEPesquisas]
        public virtual ProdutoGrade GradePrincipal { get => gradePrincipal; set => gradePrincipal = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
