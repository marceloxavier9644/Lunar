using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Empresa")]
    public class Empresa : ObjetoPadrao
    {
        private int id;
        private string razaoSocial;
        private string nomeFantasia;
        private string cnpj;
        private string inscricaoEstadual;
        private string responsavel;
        private string cpfResponsavel;
        private string funcaoResponsavel;
        private string contador;
        private string cpfContador;
        private string crcContador;
        private string emailContador;
        private DateTime validadeLicenca;
        private string token;
        private string cnae;
        private DateTime dataAbertura;
        private string email;
        private Endereco endereco;
        private string dddPrincipal;
        private string telefonePrincipal;
        private string dddSecundario;
        private string telefoneSecundario;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Razão Social")]
        public virtual string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        [Anotacao("Nome Fantasia")]
        public virtual string NomeFantasia { get => nomeFantasia; set => nomeFantasia = value; }
        [Anotacao("CNPJ")]
        public virtual string Cnpj { get => cnpj; set => cnpj = value; }
        [Anotacao("Inscrição Estadual")]
        public virtual string InscricaoEstadual { get => inscricaoEstadual; set => inscricaoEstadual = value; }
        [Anotacao("Responsável")]
        public virtual string Responsavel { get => responsavel; set => responsavel = value; }
        [Anotacao("CPF")]
        public virtual string CpfResponsavel { get => cpfResponsavel; set => cpfResponsavel = value; }
        [Anotacao("Função")]
        public virtual string FuncaoResponsavel { get => funcaoResponsavel; set => funcaoResponsavel = value; }
        [Anotacao("Contador")]
        public virtual string Contador { get => contador; set => contador = value; }
        [Anotacao("CPF Contador")]
        public virtual string CpfContador { get => cpfContador; set => cpfContador = value; }
        [Anotacao("CRC Contador")]
        public virtual string CrcContador { get => crcContador; set => crcContador = value; }
        [Anotacao("E-mail Contador")]
        public virtual string EmailContador { get => emailContador; set => emailContador = value; }
        [Anotacao("Validade")]
        [OcultarEmGridsEPesquisas]
        public virtual DateTime ValidadeLicenca { get => validadeLicenca; set => validadeLicenca = value; }
        [Anotacao("Token")]
        [OcultarEmGridsEPesquisas]
        public virtual string Token { get => token; set => token = value; }
        [Anotacao("CNAE")]
        public virtual string Cnae { get => cnae; set => cnae = value; }
        [Anotacao("Data Abertura")]
        public virtual DateTime DataAbertura { get => dataAbertura; set => dataAbertura = value; }
        [Anotacao("E-mail")]
        public virtual string Email { get => email; set => email = value; }
        [Anotacao("Endereço")]
        public virtual Endereco Endereco { get => endereco; set => endereco = value; }
        [Anotacao("DDD")]
        public virtual string DddPrincipal { get => dddPrincipal; set => dddPrincipal = value; }
        [Anotacao("Telefone")]
        public virtual string TelefonePrincipal { get => telefonePrincipal; set => telefonePrincipal = value; }
        [Anotacao("DDD")]
        public virtual string DddSecundario { get => dddSecundario; set => dddSecundario = value; }
        [Anotacao("Telefone")]
        public virtual string TelefoneSecundario { get => telefoneSecundario; set => telefoneSecundario = value; }
    }
}
