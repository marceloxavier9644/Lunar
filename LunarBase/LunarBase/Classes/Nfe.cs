using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("NFE")]
    public class Nfe : ObjetoPadrao
    {
        private int id;
        private string tipoOperacao;
        private string cnpjEmitente;
        private string razaoEmitente;
        private string cUf;
        private string cNf;
        private string natOp;
        private string modelo;
        private string serie;
        private string nNf;
        private string dhEmi;
        private string dhSaiEnt;
        private string tpNf;
        private string idDest;
        private string cMunFg;
        private string tpImp;
        private string tpEmis;
        private string cDv;
        private string tpAmb;
        private string finNfe;
        private string indFinal;
        private string indPres;
        private string indIntermed;
        private string procEmi;
        private string verProc;
        private string chave;

        private decimal vBc;
        private decimal vIcms;
        private decimal vIcmsDeson;
        private decimal vFcp;
        private decimal vBcst;
        private decimal vSt;
        private decimal vFcpst;
        private decimal vFcpstRet;
        private decimal vProd;
        private decimal vFrete;
        private decimal vSeg;
        private decimal vDesc;
        private decimal vIi;
        private decimal vIpi;
        private decimal vIpiDevol;
        private decimal vPis;
        private decimal vCofins;
        private decimal vOutro;
        private decimal vNf;
        private decimal vTotTrib;

        private string modFrete;
        private string infCpl;

        private bool lancada;
        private DateTime dataLancamento;
        private DateTime dataEmissao;
        private bool conciliado;

        private EmpresaFilial empresaFilial;
        private int nsu;

        private string manifesto;
        private string status;
        private string codStatus;
        private string protocolo;
        private DateTime dataProtocolo;
        private bool cancelada;
        private string destinatario;
        private string cnpjDestinatario;
        private NfeStatus nfeStatus;
        private string xml;
        private Pessoa cliente;
        private Pessoa fornecedor;
        private string nsNrec;
        private bool possuiCartaCorrecao;

        private DateTime dataSaida;
        private bool movimentaEstoque;
        private bool movimentaFinanceiro;
        private Pessoa transportadora;
        private string codigoAntt;
        private string placa;
        private string especie;
        private string volume;
        private string marca;
        private string pesoBruto;
        private string pesoLiquido;
        private NaturezaOperacao naturezaOperacao;
        private string idInut;
        private bool nuvem;
        private bool notaAgrupada;

        [Anotacao("ID")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Tipo Operação")]
        [OcultarEmGridsEPesquisas]
        public virtual string TipoOperacao { get => tipoOperacao; set => tipoOperacao = value; }
        [Anotacao("CUf")]
        [OcultarEmGridsEPesquisas]
        public virtual string CUf { get => cUf; set => cUf = value; }
        [Anotacao("CNf")]
        [OcultarEmGridsEPesquisas]
        public virtual string CNf { get => cNf; set => cNf = value; }
        [Anotacao("NatOp")]
        public virtual string NatOp { get => natOp; set => natOp = value; }
        [Anotacao("Modelo")]
        public virtual string Modelo { get => modelo; set => modelo = value; }
        [Anotacao("Serie")]
        public virtual string Serie { get => serie; set => serie = value; }
        [Anotacao("NNf")]
        public virtual string NNf { get => nNf; set => nNf = value; }
        [Anotacao("DhEmi")]
        [OcultarEmGridsEPesquisas]
        public virtual string DhEmi { get => dhEmi; set => dhEmi = value; }
        [Anotacao("DhSaiEnt")]
        [OcultarEmGridsEPesquisas]
        public virtual string DhSaiEnt { get => dhSaiEnt; set => dhSaiEnt = value; }
        [Anotacao("TpNf")]
        [OcultarEmGridsEPesquisas]
        public virtual string TpNf { get => tpNf; set => tpNf = value; }
        [Anotacao("IdDest")]
        [OcultarEmGridsEPesquisas]
        public virtual string IdDest { get => idDest; set => idDest = value; }
        [Anotacao("CMunFg")]
        [OcultarEmGridsEPesquisas]
        public virtual string CMunFg { get => cMunFg; set => cMunFg = value; }
        [Anotacao("TpImp")]
        [OcultarEmGridsEPesquisas]
        public virtual string TpImp { get => tpImp; set => tpImp = value; }
        [Anotacao("TpEmis")]
        [OcultarEmGridsEPesquisas]
        public virtual string TpEmis { get => tpEmis; set => tpEmis = value; }
        [Anotacao("CDv")]
        [OcultarEmGridsEPesquisas]
        public virtual string CDv { get => cDv; set => cDv = value; }
        [Anotacao("TpAmb")]
        [OcultarEmGridsEPesquisas]
        public virtual string TpAmb { get => tpAmb; set => tpAmb = value; }
        [Anotacao("FinNfe")]
        [OcultarEmGridsEPesquisas]
        public virtual string FinNfe { get => finNfe; set => finNfe = value; }
        [Anotacao("IndFinal")]
        [OcultarEmGridsEPesquisas]
        public virtual string IndFinal { get => indFinal; set => indFinal = value; }
        [Anotacao("IndPres")]
        [OcultarEmGridsEPesquisas]
        public virtual string IndPres { get => indPres; set => indPres = value; }
        [Anotacao("IndIntermed")]
        [OcultarEmGridsEPesquisas]
        public virtual string IndIntermed { get => indIntermed; set => indIntermed = value; }
        [Anotacao("ProcEmi")]
        [OcultarEmGridsEPesquisas]
        public virtual string ProcEmi { get => procEmi; set => procEmi = value; }
        [Anotacao("VerProc")]
        [OcultarEmGridsEPesquisas]
        public virtual string VerProc { get => verProc; set => verProc = value; }
        [Anotacao("Chave")]
        public virtual string Chave { get => chave; set => chave = value; }
        [Anotacao("VBc")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VBc { get => vBc; set => vBc = value; }
        [Anotacao("VIcms")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VIcms { get => vIcms; set => vIcms = value; }
        [Anotacao("VIcmsDeson")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VIcmsDeson { get => vIcmsDeson; set => vIcmsDeson = value; }
        [Anotacao("VFcp")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VFcp { get => vFcp; set => vFcp = value; }
        [Anotacao("VBcst")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VBcst { get => vBcst; set => vBcst = value; }
        [Anotacao("VSt")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VSt { get => vSt; set => vSt = value; }
        [Anotacao("VFcpst")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VFcpst { get => vFcpst; set => vFcpst = value; }
        [Anotacao("VFcpstRet")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VFcpstRet { get => vFcpstRet; set => vFcpstRet = value; }
        [Anotacao("VProd")]
        public virtual decimal VProd { get => vProd; set => vProd = value; }
        [Anotacao("VFrete")]
        public virtual decimal VFrete { get => vFrete; set => vFrete = value; }
        [Anotacao("VSeg")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VSeg { get => vSeg; set => vSeg = value; }
        [Anotacao("VDesc")]
        public virtual decimal VDesc { get => vDesc; set => vDesc = value; }
        [Anotacao("VIi")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VIi { get => vIi; set => vIi = value; }
        [Anotacao("VIpi")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VIpi { get => vIpi; set => vIpi = value; }
        [Anotacao("VIpiDevol")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VIpiDevol { get => vIpiDevol; set => vIpiDevol = value; }
        [Anotacao("VPis")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VPis { get => vPis; set => vPis = value; }
        [Anotacao("VCofins")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VCofins { get => vCofins; set => vCofins = value; }
        [Anotacao("VOutro")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VOutro { get => vOutro; set => vOutro = value; }
        [Anotacao("VNf")]
        public virtual decimal VNf { get => vNf; set => vNf = value; }
        [Anotacao("ModFrete")]
        [OcultarEmGridsEPesquisas]
        public virtual string ModFrete { get => modFrete; set => modFrete = value; }
        [Anotacao("InfCpl")]
        [OcultarEmGridsEPesquisas]
        public virtual string InfCpl { get => infCpl; set => infCpl = value; }
        [Anotacao("Lancada")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Lancada { get => lancada; set => lancada = value; }
        [Anotacao("DataLancamento")]
        [OcultarEmGridsEPesquisas]
        public virtual DateTime DataLancamento { get => dataLancamento; set => dataLancamento = value; }
        [Anotacao("Conciliado")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Conciliado { get => conciliado; set => conciliado = value; }
        [Anotacao("EmpresaFilial")]
        [OcultarEmGridsEPesquisas]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Nsu")]
        [OcultarEmGridsEPesquisas]
        public virtual int Nsu { get => nsu; set => nsu = value; }
        [Anotacao("DataEmissao")]
        public virtual DateTime DataEmissao { get => dataEmissao; set => dataEmissao = value; }
        [Anotacao("CnpjEmitente")]
        [OcultarEmGridsEPesquisas]
        public virtual string CnpjEmitente { get => cnpjEmitente; set => cnpjEmitente = value; }
        [Anotacao("RazaoEmitente")]
        [OcultarEmGridsEPesquisas]
        public virtual string RazaoEmitente { get => razaoEmitente; set => razaoEmitente = value; }
        [Anotacao("Manifesto")]
        [OcultarEmGridsEPesquisas]
        public virtual string Manifesto { get => manifesto; set => manifesto = value; }
        [Anotacao("VTotTrib")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal VTotTrib { get => vTotTrib; set => vTotTrib = value; }
        [Anotacao("Status")]
        [OcultarEmGridsEPesquisas]
        public virtual string Status { get => status; set => status = value; }
        [Anotacao("CodStatus")]
        [OcultarEmGridsEPesquisas]
        public virtual string CodStatus { get => codStatus; set => codStatus = value; }
        [Anotacao("Protocolo")]
        public virtual string Protocolo { get => protocolo; set => protocolo = value; }
        [Anotacao("DataProtocolo")]
        [OcultarEmGridsEPesquisas]
        public virtual DateTime DataProtocolo { get => dataProtocolo; set => dataProtocolo = value; }
        [Anotacao("Destinatario")]
        public virtual string Destinatario { get => destinatario; set => destinatario = value; }
        [Anotacao("CnpjDestinatario")]
        public virtual string CnpjDestinatario { get => cnpjDestinatario; set => cnpjDestinatario = value; }
        [Anotacao("Cancelada")]
        public virtual bool Cancelada { get => cancelada; set => cancelada = value; }
        [Anotacao("NfeStatus")]
        [OcultarEmGridsEPesquisas]
        public virtual NfeStatus NfeStatus { get => nfeStatus; set => nfeStatus = value; }
        [Anotacao("Xml")]
        [OcultarEmGridsEPesquisas]
        public virtual string Xml { get => xml; set => xml = value; }
        [Anotacao("Cliente")]
        [OcultarEmGridsEPesquisas]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Fornecedor")]
        [OcultarEmGridsEPesquisas]
        public virtual Pessoa Fornecedor { get => fornecedor; set => fornecedor = value; }
        [Anotacao("NsNrec")]
        [OcultarEmGridsEPesquisas]
        public virtual string NsNrec { get => nsNrec; set => nsNrec = value; }
        [Anotacao("PossuiCartaCorrecao")]
        [OcultarEmGridsEPesquisas]
        public virtual bool PossuiCartaCorrecao { get => possuiCartaCorrecao; set => possuiCartaCorrecao = value; }
        [Anotacao("Data Saída")]
        [OcultarEmGridsEPesquisas]
        public virtual DateTime DataSaida { get => dataSaida; set => dataSaida = value; }
        [Anotacao("Movimenta Estoque")]
        [OcultarEmGridsEPesquisas]
        public virtual bool MovimentaEstoque { get => movimentaEstoque; set => movimentaEstoque = value; }
        [Anotacao("Movimenta Financeiro")]
        [OcultarEmGridsEPesquisas]
        public virtual bool MovimentaFinanceiro { get => movimentaFinanceiro; set => movimentaFinanceiro = value; }
        [Anotacao("Transportadora")]
        [OcultarEmGridsEPesquisas]
        public virtual Pessoa Transportadora { get => transportadora; set => transportadora = value; }
        [Anotacao("Codigo ANTT")]
        [OcultarEmGridsEPesquisas]
        public virtual string CodigoAntt { get => codigoAntt; set => codigoAntt = value; }
        [Anotacao("Placa")]
        [OcultarEmGridsEPesquisas]
        public virtual string Placa { get => placa; set => placa = value; }
        [Anotacao("Especie")]
        [OcultarEmGridsEPesquisas]
        public virtual string Especie { get => especie; set => especie = value; }
        [Anotacao("Volumes")]
        [OcultarEmGridsEPesquisas]
        public virtual string Volume { get => volume; set => volume = value; }
        [Anotacao("Marca")]
        [OcultarEmGridsEPesquisas]
        public virtual string Marca { get => marca; set => marca = value; }
        [Anotacao("Peso Bruto")]
        [OcultarEmGridsEPesquisas]
        public virtual string PesoBruto { get => pesoBruto; set => pesoBruto = value; }
        [Anotacao("Peso Liquido")]
        [OcultarEmGridsEPesquisas]
        public virtual string PesoLiquido { get => pesoLiquido; set => pesoLiquido = value; }
        [Anotacao("Natureza Operacao")]
        [OcultarEmGridsEPesquisas]
        public virtual NaturezaOperacao NaturezaOperacao { get => naturezaOperacao; set => naturezaOperacao = value; }
        [Anotacao("ID Inut")]
        [OcultarEmGridsEPesquisas]
        public virtual string IdInut { get => idInut; set => idInut = value; }
        [Anotacao("Nuvem")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Nuvem { get => nuvem; set => nuvem = value; }
        [Anotacao("Nota Agrupada")]
        [OcultarEmGridsEPesquisas]
        public virtual bool NotaAgrupada { get => notaAgrupada; set => notaAgrupada = value; }
    }
}
