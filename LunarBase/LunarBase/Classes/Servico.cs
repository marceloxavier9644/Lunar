using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Serviço")]
    public class Servico : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private decimal valor;
        private string codigoServicoNfse;
        private string aliquotaIss;
        private UnidadeMedida unidadeMedida;
        private EmpresaFilial filial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Serviço")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Valor")]
        public virtual decimal Valor { get => valor; set => valor = value; }
        [Anotacao("Unidade de Medida")]
        public virtual UnidadeMedida UnidadeMedida { get => unidadeMedida; set => unidadeMedida = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial Filial { get => filial; set => filial = value; }
        [Anotacao("Codigo Servico")]
        public virtual string CodigoServicoNfse { get => codigoServicoNfse; set => codigoServicoNfse = value; }
        [Anotacao("Aliquota")]
        public virtual string AliquotaIss { get => aliquotaIss; set => aliquotaIss = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
