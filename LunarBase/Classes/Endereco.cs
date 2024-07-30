using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Endereço")]
    public class Endereco : ObjetoPadrao
    {
        private int id;
        private string cep;
        private string logradouro;
        private string numero;
        private string complemento;
        private string referencia;
        private Cidade cidade;
        private string bairro;
        private Pessoa pessoa;
        private EmpresaFilial empresaFilial;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("CEP")]
        public virtual string Cep { get => cep; set => cep = value; }
        [Anotacao("Endereço")]
        public virtual string Logradouro { get => logradouro; set => logradouro = value; }
        [Anotacao("Número")]
        public virtual string Numero { get => numero; set => numero = value; }
        [Anotacao("Complemento")]
        public virtual string Complemento { get => complemento; set => complemento = value; }
        [Anotacao("Referência")]
        public virtual string Referencia { get => referencia; set => referencia = value; }
        [Anotacao("Cidade")]
        public virtual Cidade Cidade { get => cidade; set => cidade = value; }
        [Anotacao("Bairro")]
        public virtual string Bairro { get => bairro; set => bairro = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Empresa Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }


        public override string ToString()
        {
            return logradouro;
        }
    }
}
