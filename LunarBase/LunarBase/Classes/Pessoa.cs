using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Pessoa")]
    public class Pessoa : ObjetoPadrao
    {
        private int id;
        private string razaoSocial;
        private string nomeFantasia;
        private string cnpj;
        private string inscricaoEstadual;
        private DateTime dataNascimento;
        private string pai;
        private string mae;
        private string rg;
        private string email;
        private Endereco enderecoPrincipal;
        private PessoaTelefone pessoaTelefone;
        private bool cliente;
        private bool fornecedor;
        private bool transportadora;
        private bool funcionario;
        private bool vendedor;
        private bool tecnico;
        private string sexo;
        private string localTrabalho;
        private string funcaoTrabalho;
        private string telefoneTrabalho;
        private string salarioTrabalho;
        private string tempoTrabalho;
        private string contatoTrabalho;
        private decimal limiteCredito;
        private string tipoPessoa;
        private string observacoes;
        private bool receberLembrete;
        private string codigoImportacao;
        private string tipoParceiroImportado;
        private bool registradoSpc;
        private bool escritorioCobranca;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Nome/Razão")]
        public virtual string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        [Anotacao("Nome Fantasia/Apelido")]
        public virtual string NomeFantasia { get => nomeFantasia; set => nomeFantasia = value; }
        [Anotacao("CNPJ/CPF")]
        public virtual string Cnpj { get => cnpj; set => cnpj = value; }
        [Anotacao("Inscrição Estadual")]
        public virtual string InscricaoEstadual { get => inscricaoEstadual; set => inscricaoEstadual = value; }
        [Anotacao("pai")]
        public virtual string Pai { get => pai; set => pai = value; }
        [Anotacao("Mãe")]
        public virtual string Mae { get => mae; set => mae = value; }
        [Anotacao("RG")]
        public virtual string Rg { get => rg; set => rg = value; }
        [Anotacao("E-mail")]
        public virtual string Email { get => email; set => email = value; }
        [Anotacao("Endereço Principal")]
        public virtual Endereco EnderecoPrincipal { get => enderecoPrincipal; set => enderecoPrincipal = value; }
        [Anotacao("Fone Principal")]
        public virtual PessoaTelefone PessoaTelefone { get => pessoaTelefone; set => pessoaTelefone = value; }
        [Anotacao("Cliente")]
        public virtual bool Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Fornecedor")]
        public virtual bool Fornecedor { get => fornecedor; set => fornecedor = value; }
        [Anotacao("Transportadora")]
        public virtual bool Transportadora { get => transportadora; set => transportadora = value; }
        [Anotacao("Funcionário")]
        public virtual bool Funcionario { get => funcionario; set => funcionario = value; }
        [Anotacao("Vendedor")]
        public virtual bool Vendedor { get => vendedor; set => vendedor = value; }
        [Anotacao("Técnico")]
        public virtual bool Tecnico { get => tecnico; set => tecnico = value; }
        [Anotacao("Sexo")]
        public virtual string Sexo { get => sexo; set => sexo = value; }
        [Anotacao("Local de Trabalho")]
        public virtual string LocalTrabalho { get => localTrabalho; set => localTrabalho = value; }
        [Anotacao("Função")]
        [OcultarEmGridsEPesquisas]
        public virtual string FuncaoTrabalho { get => funcaoTrabalho; set => funcaoTrabalho = value; }
        [Anotacao("Telefone do Trabalho")]
        [OcultarEmGridsEPesquisas]
        public virtual string TelefoneTrabalho { get => telefoneTrabalho; set => telefoneTrabalho = value; }
        [Anotacao("Salário")]
        [OcultarEmGridsEPesquisas]
        public virtual string SalarioTrabalho { get => salarioTrabalho; set => salarioTrabalho = value; }
        [Anotacao("Tempo no Emprego")]
        [OcultarEmGridsEPesquisas]
        public virtual string TempoTrabalho { get => tempoTrabalho; set => tempoTrabalho = value; }
        [Anotacao("Contato Trabalho")]
        [OcultarEmGridsEPesquisas]
        public virtual string ContatoTrabalho { get => contatoTrabalho; set => contatoTrabalho = value; }
        [Anotacao("Limite de Crédito")]
        public virtual decimal LimiteCredito { get => limiteCredito; set => limiteCredito = value; }
        [Anotacao("Tipo de Pessoa")]
        public virtual string TipoPessoa { get => tipoPessoa; set => tipoPessoa = value; }
        [Anotacao("Data Nascimento")]
        public virtual DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        [Anotacao("Observacoes")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Lembrete")]
        public virtual bool ReceberLembrete { get => receberLembrete; set => receberLembrete = value; }
        [Anotacao("Cód Importação")]
        public virtual string CodigoImportacao { get => codigoImportacao; set => codigoImportacao = value; }
        [Anotacao("Tipo Parceiro")]
        public virtual string TipoParceiroImportado { get => tipoParceiroImportado; set => tipoParceiroImportado = value; }
        [Anotacao("Registrado SPC")]
        public virtual bool RegistradoSpc { get => registradoSpc; set => registradoSpc = value; }
        [Anotacao("Escritorio Cobranca")]
        public virtual bool EscritorioCobranca { get => escritorioCobranca; set => escritorioCobranca = value; }

        public override string ToString()
        {
            return razaoSocial;
        }

        //regime estadual e federal
        //autorizados
        //Contas bancarias
        //emailnotafiscal
    }
}
