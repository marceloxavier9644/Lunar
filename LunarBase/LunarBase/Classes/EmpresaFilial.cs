using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Filial")]
    public class EmpresaFilial : ObjetoPadrao
    {
        private int id;
        private string razaoSocial;
        private string nomeFantasia;
        private string cnpj;
        private string inscricaoEstadual;
        private Endereco endereco;
        private Empresa empresa;
        private string cnae;
        private DateTime dataAbertura;
        private string email;
        private string dddPrincipal;
        private string telefonePrincipal;
        private string dddSecundario;
        private string telefoneSecundario;
        private RegimeEmpresa regimeEmpresa = new RegimeEmpresa();
        private string senhaCertificado;
        private bool otica;
        private string emailXml;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Razão Social")]
        public virtual string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        [Anotacao("Nome Fantasia")]
        public virtual string NomeFantasia { get => nomeFantasia; set => nomeFantasia = value; }
        [Anotacao("CNPJ")]
        public virtual string Cnpj { get => cnpj; set => cnpj = value; }
        [Anotacao("IE")]
        public virtual string InscricaoEstadual { get => inscricaoEstadual; set => inscricaoEstadual = value; }
        [Anotacao("Endereço")]
        public virtual Endereco Endereco { get => endereco; set => endereco = value; }
        [Anotacao("Empresa")]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }
        [Anotacao("CNAE")]
        public virtual string Cnae { get => cnae; set => cnae = value; }
        [Anotacao("Data Abertura")]
        public virtual DateTime DataAbertura { get => dataAbertura; set => dataAbertura = value; }
        [Anotacao("E-mail")]
        public virtual string Email { get => email; set => email = value; }
        [Anotacao("DDD")]
        public virtual string DddPrincipal { get => dddPrincipal; set => dddPrincipal = value; }
        [Anotacao("Telefone")]
        public virtual string TelefonePrincipal { get => telefonePrincipal; set => telefonePrincipal = value; }
        [Anotacao("DDD")]
        public virtual string DddSecundario { get => dddSecundario; set => dddSecundario = value; }
        [Anotacao("Telefone")]
        public virtual string TelefoneSecundario { get => telefoneSecundario; set => telefoneSecundario = value; }
        [Anotacao("Telefone")]
        public virtual RegimeEmpresa RegimeEmpresa { get => regimeEmpresa; set => regimeEmpresa = value; }
        [Anotacao("Senha Certificado")]
        public virtual string SenhaCertificado { get => senhaCertificado; set => senhaCertificado = value; }
        [Anotacao("Otica")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Otica { get => otica; set => otica = value; }
        [Anotacao("Email XML")]
        [OcultarEmGridsEPesquisas]
        public virtual string EmailXml { get => emailXml; set => emailXml = value; }

        public override string ToString()
        {
            return nomeFantasia;
        }
    }
}
