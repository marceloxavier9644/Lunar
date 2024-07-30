using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Anexo")]
    public class Anexo : ObjetoPadrao
    {
        private int id;
        private string codigo;
        private string caminho;
        private EmpresaFilial filial;
        private OrdemServico ordemServico;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Código")]
        public virtual string Codigo { get => codigo; set => codigo = value; }
        [Anotacao("Caminho")]
        public virtual string Caminho { get => caminho; set => caminho = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial Filial { get => filial; set => filial = value; }
        [Anotacao("Ordem de Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
    }
}
