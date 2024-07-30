using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Referência Comercial")]
    public class PessoaReferenciaComercial : ObjetoPadrao
    {
        private int id;
        private string empresa;
        private string telefone;
        private string observacoes;
        private Pessoa pessoa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Empresa")]
        public virtual string Empresa { get => empresa; set => empresa = value; }
        [Anotacao("Telefone")]
        public virtual string Telefone { get => telefone; set => telefone = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Cliente")]
        [OcultarEmGridsEPesquisas]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        public override string ToString()
        {
            return empresa;
        }
    }
}
