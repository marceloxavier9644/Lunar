using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("SPC")]
    public class Spc : ObjetoPadrao
    {
        private int id;
        private string nomeUsuario;
        private string localRegistro;
        private DateTime dataRegistro;
        private decimal valorRegistro;
        private DateTime dataConsulta;
        private string loginWebService;
        private double quantidadeRegistro;
        private Pessoa pessoa;
        private EmpresaFilial empresaFilial;
        private string protocoloConsulta;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Usuario Sistema")]
        public virtual string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        [Anotacao("Local Registro")]
        public virtual string LocalRegistro { get => localRegistro; set => localRegistro = value; }
        [Anotacao("Data Registro")]
        public virtual DateTime DataRegistro { get => dataRegistro; set => dataRegistro = value; }
        [Anotacao("Valor")]
        public virtual decimal ValorRegistro { get => valorRegistro; set => valorRegistro = value; }
        [Anotacao("Data Consulta")]
        public virtual DateTime DataConsulta { get => dataConsulta; set => dataConsulta = value; }
        [Anotacao("Login Web Service")]
        public virtual string LoginWebService { get => loginWebService; set => loginWebService = value; }
        [Anotacao("Qtd Registro na Consulta")]
        public virtual double QuantidadeRegistro { get => quantidadeRegistro; set => quantidadeRegistro = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Protocolo")]
        public virtual string ProtocoloConsulta { get => protocoloConsulta; set => protocoloConsulta = value; }
    }
}
