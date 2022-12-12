using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades.LunarNFe.Autorizacao
{

    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public partial class NFe
    {

        private NFeInfNFe infNFeField;

        /// <remarks/>
        public NFeInfNFe infNFe
        {
            get
            {
                return this.infNFeField;
            }
            set
            {
                this.infNFeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFe
    {

        private NFeInfNFeIde ideField;

        private NFeInfNFeEmit emitField;

        private NFeInfNFeDest destField;

        private NFeInfNFeRetirada retiradaField;

        private NFeInfNFeEntrega entregaField;

        private NFeInfNFeDet[] detField;

        private NFeInfNFeTotal totalField;

        private NFeInfNFeTransp transpField;

        private NFeInfNFeInfAdic infAdicField;

        private string idField;

        private decimal versaoField;

        /// <remarks/>
        public NFeInfNFeIde ide
        {
            get
            {
                return this.ideField;
            }
            set
            {
                this.ideField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeEmit emit
        {
            get
            {
                return this.emitField;
            }
            set
            {
                this.emitField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeDest dest
        {
            get
            {
                return this.destField;
            }
            set
            {
                this.destField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeRetirada retirada
        {
            get
            {
                return this.retiradaField;
            }
            set
            {
                this.retiradaField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeEntrega entrega
        {
            get
            {
                return this.entregaField;
            }
            set
            {
                this.entregaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("det")]
        public NFeInfNFeDet[] det
        {
            get
            {
                return this.detField;
            }
            set
            {
                this.detField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTotal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTransp transp
        {
            get
            {
                return this.transpField;
            }
            set
            {
                this.transpField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeInfAdic infAdic
        {
            get
            {
                return this.infAdicField;
            }
            set
            {
                this.infAdicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeIde
    {

        private byte cUFField;

        private uint cNFField;

        private string natOpField;

        private byte indPagField;

        private byte modField;

        private byte serieField;

        private byte nNFField;

        private System.DateTime dEmiField;

        private System.DateTime dSaiEntField;

        private byte tpNFField;

        private uint cMunFGField;

        private byte tpImpField;

        private byte tpEmisField;

        private byte cDVField;

        private byte tpAmbField;

        private byte finNFeField;

        private byte procEmiField;

        private string verProcField;

        /// <remarks/>
        public byte cUF
        {
            get
            {
                return this.cUFField;
            }
            set
            {
                this.cUFField = value;
            }
        }

        /// <remarks/>
        public uint cNF
        {
            get
            {
                return this.cNFField;
            }
            set
            {
                this.cNFField = value;
            }
        }

        /// <remarks/>
        public string natOp
        {
            get
            {
                return this.natOpField;
            }
            set
            {
                this.natOpField = value;
            }
        }

        /// <remarks/>
        public byte indPag
        {
            get
            {
                return this.indPagField;
            }
            set
            {
                this.indPagField = value;
            }
        }

        /// <remarks/>
        public byte mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
            }
        }

        /// <remarks/>
        public byte serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        public byte nNF
        {
            get
            {
                return this.nNFField;
            }
            set
            {
                this.nNFField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dEmi
        {
            get
            {
                return this.dEmiField;
            }
            set
            {
                this.dEmiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dSaiEnt
        {
            get
            {
                return this.dSaiEntField;
            }
            set
            {
                this.dSaiEntField = value;
            }
        }

        /// <remarks/>
        public byte tpNF
        {
            get
            {
                return this.tpNFField;
            }
            set
            {
                this.tpNFField = value;
            }
        }

        /// <remarks/>
        public uint cMunFG
        {
            get
            {
                return this.cMunFGField;
            }
            set
            {
                this.cMunFGField = value;
            }
        }

        /// <remarks/>
        public byte tpImp
        {
            get
            {
                return this.tpImpField;
            }
            set
            {
                this.tpImpField = value;
            }
        }

        /// <remarks/>
        public byte tpEmis
        {
            get
            {
                return this.tpEmisField;
            }
            set
            {
                this.tpEmisField = value;
            }
        }

        /// <remarks/>
        public byte cDV
        {
            get
            {
                return this.cDVField;
            }
            set
            {
                this.cDVField = value;
            }
        }

        /// <remarks/>
        public byte tpAmb
        {
            get
            {
                return this.tpAmbField;
            }
            set
            {
                this.tpAmbField = value;
            }
        }

        /// <remarks/>
        public byte finNFe
        {
            get
            {
                return this.finNFeField;
            }
            set
            {
                this.finNFeField = value;
            }
        }

        /// <remarks/>
        public byte procEmi
        {
            get
            {
                return this.procEmiField;
            }
            set
            {
                this.procEmiField = value;
            }
        }

        /// <remarks/>
        public string verProc
        {
            get
            {
                return this.verProcField;
            }
            set
            {
                this.verProcField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeEmit
    {

        private ulong cNPJField;

        private string xNomeField;

        private string xFantField;

        private NFeInfNFeEmitEnderEmit enderEmitField;

        private ulong ieField;

        /// <remarks/>
        public ulong CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
            }
        }

        /// <remarks/>
        public string xFant
        {
            get
            {
                return this.xFantField;
            }
            set
            {
                this.xFantField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeEmitEnderEmit enderEmit
        {
            get
            {
                return this.enderEmitField;
            }
            set
            {
                this.enderEmitField = value;
            }
        }

        /// <remarks/>
        public ulong IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeEmitEnderEmit
    {

        private string xLgrField;

        private byte nroField;

        private string xCplField;

        private string xBairroField;

        private uint cMunField;

        private string xMunField;

        private string ufField;

        private uint cEPField;

        private ushort cPaisField;

        private string xPaisField;

        private uint foneField;

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
            }
        }

        /// <remarks/>
        public byte nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
            }
        }

        /// <remarks/>
        public uint cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public uint CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
            }
        }

        /// <remarks/>
        public ushort cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
            }
        }

        /// <remarks/>
        public string xPais
        {
            get
            {
                return this.xPaisField;
            }
            set
            {
                this.xPaisField = value;
            }
        }

        /// <remarks/>
        public uint fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDest
    {

        private byte cNPJField;

        private string xNomeField;

        private NFeInfNFeDestEnderDest enderDestField;

        private object ieField;

        /// <remarks/>
        public byte CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeDestEnderDest enderDest
        {
            get
            {
                return this.enderDestField;
            }
            set
            {
                this.enderDestField = value;
            }
        }

        /// <remarks/>
        public object IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDestEnderDest
    {

        private string xLgrField;

        private ushort nroField;

        private string xCplField;

        private string xBairroField;

        private uint cMunField;

        private string xMunField;

        private string ufField;

        private uint cEPField;

        private ushort cPaisField;

        private string xPaisField;

        private uint foneField;

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
            }
        }

        /// <remarks/>
        public ushort nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
            }
        }

        /// <remarks/>
        public uint cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public uint CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
            }
        }

        /// <remarks/>
        public ushort cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
            }
        }

        /// <remarks/>
        public string xPais
        {
            get
            {
                return this.xPaisField;
            }
            set
            {
                this.xPaisField = value;
            }
        }

        /// <remarks/>
        public uint fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeRetirada
    {

        private ulong cNPJField;

        private string xLgrField;

        private ushort nroField;

        private string xCplField;

        private string xBairroField;

        private uint cMunField;

        private string xMunField;

        private string ufField;

        /// <remarks/>
        public ulong CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
            }
        }

        /// <remarks/>
        public ushort nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
            }
        }

        /// <remarks/>
        public uint cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeEntrega
    {

        private ulong cNPJField;

        private string xLgrField;

        private ushort nroField;

        private string xCplField;

        private string xBairroField;

        private uint cMunField;

        private string xMunField;

        private string ufField;

        /// <remarks/>
        public ulong CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
            }
        }

        /// <remarks/>
        public ushort nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
            }
        }

        /// <remarks/>
        public uint cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDet
    {

        private NFeInfNFeDetProd prodField;

        private NFeInfNFeDetImposto impostoField;

        private byte nItemField;

        /// <remarks/>
        public NFeInfNFeDetProd prod
        {
            get
            {
                return this.prodField;
            }
            set
            {
                this.prodField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeDetImposto imposto
        {
            get
            {
                return this.impostoField;
            }
            set
            {
                this.impostoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte nItem
        {
            get
            {
                return this.nItemField;
            }
            set
            {
                this.nItemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetProd
    {

        private byte cProdField;

        private object cEANField;

        private string xProdField;

        private ushort cFOPField;

        private string uComField;

        private decimal qComField;

        private byte vUnComField;

        private decimal vProdField;

        private object cEANTribField;

        private string uTribField;

        private decimal qTribField;

        private decimal vUnTribField;

        /// <remarks/>
        public byte cProd
        {
            get
            {
                return this.cProdField;
            }
            set
            {
                this.cProdField = value;
            }
        }

        /// <remarks/>
        public object cEAN
        {
            get
            {
                return this.cEANField;
            }
            set
            {
                this.cEANField = value;
            }
        }

        /// <remarks/>
        public string xProd
        {
            get
            {
                return this.xProdField;
            }
            set
            {
                this.xProdField = value;
            }
        }

        /// <remarks/>
        public ushort CFOP
        {
            get
            {
                return this.cFOPField;
            }
            set
            {
                this.cFOPField = value;
            }
        }

        /// <remarks/>
        public string uCom
        {
            get
            {
                return this.uComField;
            }
            set
            {
                this.uComField = value;
            }
        }

        /// <remarks/>
        public decimal qCom
        {
            get
            {
                return this.qComField;
            }
            set
            {
                this.qComField = value;
            }
        }

        /// <remarks/>
        public byte vUnCom
        {
            get
            {
                return this.vUnComField;
            }
            set
            {
                this.vUnComField = value;
            }
        }

        /// <remarks/>
        public decimal vProd
        {
            get
            {
                return this.vProdField;
            }
            set
            {
                this.vProdField = value;
            }
        }

        /// <remarks/>
        public object cEANTrib
        {
            get
            {
                return this.cEANTribField;
            }
            set
            {
                this.cEANTribField = value;
            }
        }

        /// <remarks/>
        public string uTrib
        {
            get
            {
                return this.uTribField;
            }
            set
            {
                this.uTribField = value;
            }
        }

        /// <remarks/>
        public decimal qTrib
        {
            get
            {
                return this.qTribField;
            }
            set
            {
                this.qTribField = value;
            }
        }

        /// <remarks/>
        public decimal vUnTrib
        {
            get
            {
                return this.vUnTribField;
            }
            set
            {
                this.vUnTribField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImposto
    {

        private NFeInfNFeDetImpostoICMS iCMSField;

        private NFeInfNFeDetImpostoPIS pISField;

        private NFeInfNFeDetImpostoCOFINS cOFINSField;

        /// <remarks/>
        public NFeInfNFeDetImpostoICMS ICMS
        {
            get
            {
                return this.iCMSField;
            }
            set
            {
                this.iCMSField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeDetImpostoPIS PIS
        {
            get
            {
                return this.pISField;
            }
            set
            {
                this.pISField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeDetImpostoCOFINS COFINS
        {
            get
            {
                return this.cOFINSField;
            }
            set
            {
                this.cOFINSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoICMS
    {

        private NFeInfNFeDetImpostoICMSICMS00 iCMS00Field;

        /// <remarks/>
        public NFeInfNFeDetImpostoICMSICMS00 ICMS00
        {
            get
            {
                return this.iCMS00Field;
            }
            set
            {
                this.iCMS00Field = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoICMSICMS00
    {

        private byte origField;

        private byte cSTField;

        private byte modBCField;

        private decimal vBCField;

        private decimal pICMSField;

        private decimal vICMSField;

        /// <remarks/>
        public byte orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
            }
        }

        /// <remarks/>
        public byte CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
            }
        }

        /// <remarks/>
        public byte modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
            }
        }

        /// <remarks/>
        public decimal vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
            }
        }

        /// <remarks/>
        public decimal pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
            }
        }

        /// <remarks/>
        public decimal vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoPIS
    {

        private NFeInfNFeDetImpostoPISPISAliq pISAliqField;

        /// <remarks/>
        public NFeInfNFeDetImpostoPISPISAliq PISAliq
        {
            get
            {
                return this.pISAliqField;
            }
            set
            {
                this.pISAliqField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoPISPISAliq
    {

        private byte cSTField;

        private decimal vBCField;

        private decimal pPISField;

        private ushort vPISField;

        /// <remarks/>
        public byte CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
            }
        }

        /// <remarks/>
        public decimal vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
            }
        }

        /// <remarks/>
        public decimal pPIS
        {
            get
            {
                return this.pPISField;
            }
            set
            {
                this.pPISField = value;
            }
        }

        /// <remarks/>
        public ushort vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoCOFINS
    {

        private NFeInfNFeDetImpostoCOFINSCOFINSAliq cOFINSAliqField;

        /// <remarks/>
        public NFeInfNFeDetImpostoCOFINSCOFINSAliq COFINSAliq
        {
            get
            {
                return this.cOFINSAliqField;
            }
            set
            {
                this.cOFINSAliqField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeDetImpostoCOFINSCOFINSAliq
    {

        private byte cSTField;

        private decimal vBCField;

        private decimal pCOFINSField;

        private decimal vCOFINSField;

        /// <remarks/>
        public byte CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
            }
        }

        /// <remarks/>
        public decimal vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
            }
        }

        /// <remarks/>
        public decimal pCOFINS
        {
            get
            {
                return this.pCOFINSField;
            }
            set
            {
                this.pCOFINSField = value;
            }
        }

        /// <remarks/>
        public decimal vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTotal
    {

        private NFeInfNFeTotalICMSTot iCMSTotField;

        /// <remarks/>
        public NFeInfNFeTotalICMSTot ICMSTot
        {
            get
            {
                return this.iCMSTotField;
            }
            set
            {
                this.iCMSTotField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTotalICMSTot
    {

        private decimal vBCField;

        private decimal vICMSField;

        private byte vBCSTField;

        private byte vSTField;

        private decimal vProdField;

        private byte vFreteField;

        private byte vSegField;

        private byte vDescField;

        private byte vIIField;

        private byte vIPIField;

        private decimal vPISField;

        private decimal vCOFINSField;

        private byte vOutroField;

        private decimal vNFField;

        /// <remarks/>
        public decimal vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
            }
        }

        /// <remarks/>
        public decimal vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
            }
        }

        /// <remarks/>
        public byte vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
            }
        }

        /// <remarks/>
        public byte vST
        {
            get
            {
                return this.vSTField;
            }
            set
            {
                this.vSTField = value;
            }
        }

        /// <remarks/>
        public decimal vProd
        {
            get
            {
                return this.vProdField;
            }
            set
            {
                this.vProdField = value;
            }
        }

        /// <remarks/>
        public byte vFrete
        {
            get
            {
                return this.vFreteField;
            }
            set
            {
                this.vFreteField = value;
            }
        }

        /// <remarks/>
        public byte vSeg
        {
            get
            {
                return this.vSegField;
            }
            set
            {
                this.vSegField = value;
            }
        }

        /// <remarks/>
        public byte vDesc
        {
            get
            {
                return this.vDescField;
            }
            set
            {
                this.vDescField = value;
            }
        }

        /// <remarks/>
        public byte vII
        {
            get
            {
                return this.vIIField;
            }
            set
            {
                this.vIIField = value;
            }
        }

        /// <remarks/>
        public byte vIPI
        {
            get
            {
                return this.vIPIField;
            }
            set
            {
                this.vIPIField = value;
            }
        }

        /// <remarks/>
        public decimal vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
            }
        }

        /// <remarks/>
        public decimal vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
            }
        }

        /// <remarks/>
        public byte vOutro
        {
            get
            {
                return this.vOutroField;
            }
            set
            {
                this.vOutroField = value;
            }
        }

        /// <remarks/>
        public decimal vNF
        {
            get
            {
                return this.vNFField;
            }
            set
            {
                this.vNFField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTransp
    {

        private byte modFreteField;

        private NFeInfNFeTranspTransporta transportaField;

        private NFeInfNFeTranspVeicTransp veicTranspField;

        private NFeInfNFeTranspReboque reboqueField;

        private NFeInfNFeTranspVol volField;

        /// <remarks/>
        public byte modFrete
        {
            get
            {
                return this.modFreteField;
            }
            set
            {
                this.modFreteField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTranspTransporta transporta
        {
            get
            {
                return this.transportaField;
            }
            set
            {
                this.transportaField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTranspVeicTransp veicTransp
        {
            get
            {
                return this.veicTranspField;
            }
            set
            {
                this.veicTranspField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTranspReboque reboque
        {
            get
            {
                return this.reboqueField;
            }
            set
            {
                this.reboqueField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTranspVol vol
        {
            get
            {
                return this.volField;
            }
            set
            {
                this.volField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTranspTransporta
    {

        private ulong cNPJField;

        private string xNomeField;

        private ulong ieField;

        private string xEnderField;

        private string xMunField;

        private string ufField;

        /// <remarks/>
        public ulong CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
            }
        }

        /// <remarks/>
        public ulong IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
            }
        }

        /// <remarks/>
        public string xEnder
        {
            get
            {
                return this.xEnderField;
            }
            set
            {
                this.xEnderField = value;
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTranspVeicTransp
    {

        private string placaField;

        private string ufField;

        private uint rNTCField;

        /// <remarks/>
        public string placa
        {
            get
            {
                return this.placaField;
            }
            set
            {
                this.placaField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public uint RNTC
        {
            get
            {
                return this.rNTCField;
            }
            set
            {
                this.rNTCField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTranspReboque
    {

        private string placaField;

        private string ufField;

        private uint rNTCField;

        /// <remarks/>
        public string placa
        {
            get
            {
                return this.placaField;
            }
            set
            {
                this.placaField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public uint RNTC
        {
            get
            {
                return this.rNTCField;
            }
            set
            {
                this.rNTCField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTranspVol
    {

        private ushort qVolField;

        private string espField;

        private string marcaField;

        private ushort nVolField;

        private decimal pesoLField;

        private decimal pesoBField;

        private NFeInfNFeTranspVolLacres lacresField;

        /// <remarks/>
        public ushort qVol
        {
            get
            {
                return this.qVolField;
            }
            set
            {
                this.qVolField = value;
            }
        }

        /// <remarks/>
        public string esp
        {
            get
            {
                return this.espField;
            }
            set
            {
                this.espField = value;
            }
        }

        /// <remarks/>
        public string marca
        {
            get
            {
                return this.marcaField;
            }
            set
            {
                this.marcaField = value;
            }
        }

        /// <remarks/>
        public ushort nVol
        {
            get
            {
                return this.nVolField;
            }
            set
            {
                this.nVolField = value;
            }
        }

        /// <remarks/>
        public decimal pesoL
        {
            get
            {
                return this.pesoLField;
            }
            set
            {
                this.pesoLField = value;
            }
        }

        /// <remarks/>
        public decimal pesoB
        {
            get
            {
                return this.pesoBField;
            }
            set
            {
                this.pesoBField = value;
            }
        }

        /// <remarks/>
        public NFeInfNFeTranspVolLacres lacres
        {
            get
            {
                return this.lacresField;
            }
            set
            {
                this.lacresField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeTranspVolLacres
    {

        private string nLacreField;

        /// <remarks/>
        public string nLacre
        {
            get
            {
                return this.nLacreField;
            }
            set
            {
                this.nLacreField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class NFeInfNFeInfAdic
    {

        private string infAdFiscoField;

        /// <remarks/>
        public string infAdFisco
        {
            get
            {
                return this.infAdFiscoField;
            }
            set
            {
                this.infAdFiscoField = value;
            }
        }
    }


}
