using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Produtos da NFe")]
    public class NfeProduto : ObjetoPadrao
    {
        private int id;
        private string codigoInterno;
        private string descricaoInterna;
        private DateTime dataEntrada;
        private string cProd;
        private string cEan;
        private string xProd;
        private string ncm;
        private string cest;
        private string cfop;
        private string cfopEntrada;
        private string uCom;
        private string uComConvertida;
        private string qCom;
        private decimal vUnCom;
        private decimal vProd;
        private string cEANTrib;
        private string uTrib;
        private double qTrib;
        private decimal vUnTrib;
        private decimal vDesc;
        private string indTot;
        private string modFrete;
        private string pesoL;
        private string pesoB;
        private string indPag;
        private string tPag;
        private string vPag;
        private decimal vFrete;
        private decimal vOutro;
        private decimal vSeguro;

        //ICMS
        private string origemIcms;
        private string cstIcms;
        private decimal vBC;
        private string pICMS;
        private decimal vICMS;
        private decimal valorIsentoIcms;
        private decimal outrosIcms;
        private decimal vICMSDeson;
        private string motDesIcms;
        private decimal vBCST;
        private string pST;
        private decimal vICMSSt;
        private decimal vICMSSubstituto;
        private decimal vICMSSTRet;
        private decimal vBCFCPST;
        private string pFCPSTRet;
        private decimal vFCPSTRet;
        private string pRedBC;
        private decimal vBCEfet;
        private string pICMSEfet;
        private decimal vICMSEfet;
        private string pMVAST;
        private string modBC;
        private string modBCST;
       
        private decimal vICMSSTDeson;
        private string motDesICMSST;
        private string pFCP;
        private string pFCPST;
        private decimal vFCPST;
        private string pRedBCST;

        private decimal vFCP;
        private decimal vBCFCP;
        private string pICMSST;

        private decimal vICMSOp;
        private string pDif;
        private decimal vICMSDif;
        private string pFCPDif;
        private decimal vFCPDif;
        private decimal vFCPEfet;
        private string pBCOp;
        private string uFST;

        private string pCredSN;
        private decimal vCredICMSSN;

        private bool modBCSpecified;

        private decimal vBCSTRet;
        private decimal vBCFCPSTRet;
        private string pRedBCEfet;

        private decimal vBCSTDest;
        private decimal vICMSSTDest;

        //IPI
        private string cstIpi;
        private decimal baseIpi;
        private string aliqIpi;
        private decimal valorIpi;
        private string codEnqIpi;
        private string codSeloIpi;
        private string qSeloIpi;
        private string cnpjProdIpi;
        private string qUnidIpi;
        private decimal vUnidIpi;
        //PIS
        private string cstPis;
        private decimal basePis;
        private string aliqPis;
        private decimal valorPis;
        private decimal valorIsentoPis;
        private string qBCProdPis;
        private decimal vAliqProdPis;

        private string indSomaPISST;

        //COFINS
        private string cstCofins;
        private decimal baseCofins;
        private string aliqCofins;
        private decimal valorCofins;
        private decimal valorIsentoCofins;
        private string qBCProdCofins;
        private decimal vAliqProdCofins;
        private string indSomaCOFINSST;
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

        private double quantidadeEntrada;
        //private string unidadeMedidaConvertida;
        private string produtoAssociado;
        private Nfe nfe;
        private Produto produto;
        private string item;

        public virtual int Id { get => id; set => id = value; }
        public virtual string CProd { get => cProd; set => cProd = value; }
        public virtual string CEan { get => cEan; set => cEan = value; }
        public virtual string XProd { get => xProd; set => xProd = value; }
        public virtual string Ncm { get => ncm; set => ncm = value; }
        public virtual string Cest { get => cest; set => cest = value; }
        public virtual string Cfop { get => cfop; set => cfop = value; }
        public virtual string UCom { get => uCom; set => uCom = value; }
        public virtual string QCom { get => qCom; set => qCom = value; }
        public virtual decimal VUnCom { get => vUnCom; set => vUnCom = value; }
        public virtual decimal VProd { get => vProd; set => vProd = value; }
        public virtual string CEANTrib { get => cEANTrib; set => cEANTrib = value; }
        public virtual string UTrib { get => uTrib; set => uTrib = value; }
        public virtual double QTrib { get => qTrib; set => qTrib = value; }
        public virtual decimal VUnTrib { get => vUnTrib; set => vUnTrib = value; }
        public virtual decimal VDesc { get => vDesc; set => vDesc = value; }
        public virtual string IndTot { get => indTot; set => indTot = value; }
        public virtual string ModFrete { get => modFrete; set => modFrete = value; }
        public virtual string PesoL { get => pesoL; set => pesoL = value; }
        public virtual string PesoB { get => pesoB; set => pesoB = value; }
        public virtual string IndPag { get => indPag; set => indPag = value; }
        public virtual string TPag { get => tPag; set => tPag = value; }
        public virtual string VPag { get => vPag; set => vPag = value; }
        public virtual string OrigemIcms { get => origemIcms; set => origemIcms = value; }
        public virtual string CstIcms { get => cstIcms; set => cstIcms = value; }
        public virtual decimal VBC { get => vBC; set => vBC = value; }
        public virtual string PICMS { get => pICMS; set => pICMS = value; }
        public virtual decimal VICMS { get => vICMS; set => vICMS = value; }
        public virtual decimal ValorIsentoIcms { get => valorIsentoIcms; set => valorIsentoIcms = value; }
        public virtual decimal OutrosIcms { get => outrosIcms; set => outrosIcms = value; }
        public virtual string CstIpi { get => cstIpi; set => cstIpi = value; }
        public virtual decimal BaseIpi { get => baseIpi; set => baseIpi = value; }
        public virtual string AliqIpi { get => aliqIpi; set => aliqIpi = value; }
        public virtual decimal ValorIpi { get => valorIpi; set => valorIpi = value; }
        public virtual string CodEnqIpi { get => codEnqIpi; set => codEnqIpi = value; }
        public virtual string CodSeloIpi { get => codSeloIpi; set => codSeloIpi = value; }
        public virtual string CstPis { get => cstPis; set => cstPis = value; }
        public virtual decimal BasePis { get => basePis; set => basePis = value; }
        public virtual string AliqPis { get => aliqPis; set => aliqPis = value; }
        public virtual decimal ValorPis { get => valorPis; set => valorPis = value; }
        public virtual decimal ValorIsentoPis { get => valorIsentoPis; set => valorIsentoPis = value; }
        public virtual string CstCofins { get => cstCofins; set => cstCofins = value; }
        public virtual decimal BaseCofins { get => baseCofins; set => baseCofins = value; }
        public virtual string AliqCofins { get => aliqCofins; set => aliqCofins = value; }
        public virtual decimal ValorCofins { get => valorCofins; set => valorCofins = value; }
        public virtual decimal ValorIsentoCofins { get => valorIsentoCofins; set => valorIsentoCofins = value; }
        public virtual string CodAnp { get => codAnp; set => codAnp = value; }
        public virtual double PercGlp { get => percGlp; set => percGlp = value; }
        public virtual double PercGnn { get => percGnn; set => percGnn = value; }
        public virtual double PercGni { get => percGni; set => percGni = value; }
        public virtual decimal ValorPartida { get => valorPartida; set => valorPartida = value; }
        public virtual decimal ValorProduto { get => valorProduto; set => valorProduto = value; }
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }
        public virtual decimal ValorFinal { get => valorFinal; set => valorFinal = value; }
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
        public virtual string CodigoInterno { get => codigoInterno; set => codigoInterno = value; }
        public virtual string DescricaoInterna { get => descricaoInterna; set => descricaoInterna = value; }
        public virtual DateTime DataEntrada { get => dataEntrada; set => dataEntrada = value; }
        public virtual Produto Produto { get => produto; set => produto = value; }
        public virtual string CfopEntrada { get => cfopEntrada; set => cfopEntrada = value; }


        public virtual decimal VBCST { get => vBCST; set => vBCST = value; }
        public virtual string PST { get => pST; set => pST = value; }
        public virtual decimal VICMSSt { get => vICMSSt; set => vICMSSt = value; }
        public virtual decimal VICMSSTRet { get => vICMSSTRet; set => vICMSSTRet = value; }
        public virtual decimal VBCFCPST { get => vBCFCPST; set => vBCFCPST = value; }
        public virtual string PFCPSTRet { get => pFCPSTRet; set => pFCPSTRet = value; }
        public virtual decimal VFCPSTRet { get => vFCPSTRet; set => vFCPSTRet = value; }
        public virtual string PRedBC { get => pRedBC; set => pRedBC = value; }
        public virtual decimal VBCEfet { get => vBCEfet; set => vBCEfet = value; }
        public virtual string PICMSEfet { get => pICMSEfet; set => pICMSEfet = value; }
        public virtual decimal VICMSEfet { get => vICMSEfet; set => vICMSEfet = value; }
        public virtual string ModBC { get => modBC; set => modBC = value; }
        public virtual string ModBCST { get => modBCST; set => modBCST = value; }
        public virtual string PMVAST { get => pMVAST; set => pMVAST = value; }
        
        public virtual decimal VICMSSTDeson { get => vICMSSTDeson; set => vICMSSTDeson = value; }
        public virtual string MotDesICMSST { get => motDesICMSST; set => motDesICMSST = value; }
        public virtual string PFCP { get => pFCP; set => pFCP = value; }
        public virtual string PFCPST { get => pFCPST; set => pFCPST = value; }
        public virtual string PRedBCST { get => pRedBCST; set => pRedBCST = value; }
        public virtual decimal VFCPST { get => vFCPST; set => vFCPST = value; }
        public virtual decimal VFCP { get => vFCP; set => vFCP = value; }
        public virtual decimal VBCFCP { get => vBCFCP; set => vBCFCP = value; }
        public virtual string PICMSST { get => pICMSST; set => pICMSST = value; }
        public virtual decimal VICMSDeson { get => vICMSDeson; set => vICMSDeson = value; }
        public virtual string MotDesIcms { get => motDesIcms; set => motDesIcms = value; }
        
        public virtual decimal VICMSOp { get => vICMSOp; set => vICMSOp = value; }
        public virtual string PDif { get => pDif; set => pDif = value; }
        public virtual decimal VICMSDif { get => vICMSDif; set => vICMSDif = value; }
        public virtual string PFCPDif { get => pFCPDif; set => pFCPDif = value; }
        public virtual decimal VFCPDif { get => vFCPDif; set => vFCPDif = value; }
        public virtual decimal VFCPEfet { get => vFCPEfet; set => vFCPEfet = value; }
        public virtual bool ModBCSpecified { get => modBCSpecified; set => modBCSpecified = value; }
        public virtual decimal VICMSSubstituto { get => vICMSSubstituto; set => vICMSSubstituto = value; }
        
        public virtual decimal VBCSTRet { get => vBCSTRet; set => vBCSTRet = value; }
        public virtual decimal VBCFCPSTRet { get => vBCFCPSTRet; set => vBCFCPSTRet = value; }
        public virtual string PRedBCEfet { get => pRedBCEfet; set => pRedBCEfet = value; }
        public virtual string PBCOp { get => pBCOp; set => pBCOp = value; }
        public virtual string UFST { get => uFST; set => uFST = value; }
        public virtual string PCredSN { get => pCredSN; set => pCredSN = value; }
        public virtual decimal VCredICMSSN { get => vCredICMSSN; set => vCredICMSSN = value; }
        public virtual string QBCProdCofins { get => qBCProdCofins; set => qBCProdCofins = value; }
        public virtual decimal VAliqProdCofins { get => vAliqProdCofins; set => vAliqProdCofins = value; }
        public virtual string IndSomaCOFINSST { get => indSomaCOFINSST; set => indSomaCOFINSST = value; }
        public virtual decimal VBCSTDest { get => vBCSTDest; set => vBCSTDest = value; }
        public virtual decimal VICMSSTDest { get => vICMSSTDest; set => vICMSSTDest = value; }
        public virtual string QBCProdPis { get => qBCProdPis; set => qBCProdPis = value; }
        public virtual decimal VAliqProdPis { get => vAliqProdPis; set => vAliqProdPis = value; }
        public virtual string IndSomaPISST { get => indSomaPISST; set => indSomaPISST = value; }
        public virtual string QSeloIpi { get => qSeloIpi; set => qSeloIpi = value; }
        public virtual string CnpjProdIpi { get => cnpjProdIpi; set => cnpjProdIpi = value; }
        public virtual string QUnidIpi { get => qUnidIpi; set => qUnidIpi = value; }
        public virtual decimal VUnidIpi { get => vUnidIpi; set => vUnidIpi = value; }
        public virtual double QuantidadeEntrada { get => quantidadeEntrada; set => quantidadeEntrada = value; }
        //public virtual string UnidadeMedidaConvertida { get => unidadeMedidaConvertida; set => unidadeMedidaConvertida = value; }
        public virtual string ProdutoAssociado { get => produtoAssociado; set => produtoAssociado = value; }
        public virtual string Item { get => item; set => item = value; }
        public virtual decimal VFrete { get => vFrete; set => vFrete = value; }
        public virtual decimal VOutro { get => vOutro; set => vOutro = value; }
        public virtual decimal VSeguro { get => vSeguro; set => vSeguro = value; }
        public virtual string UComConvertida { get => uComConvertida; set => uComConvertida = value; }
    }
}
