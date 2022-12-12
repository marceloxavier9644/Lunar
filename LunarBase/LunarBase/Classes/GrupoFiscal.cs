using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Grupo Fiscal")]
    public class GrupoFiscal : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string cfopSaidaEstadual;
        private string cfopSaidaInterestadual;
        private string csosnSaida;
        private string aliquotaIcms;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Grupo Fiscal")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("CFOP Estadual")]
        public virtual string CfopSaidaEstadual { get => cfopSaidaEstadual; set => cfopSaidaEstadual = value; }
        [Anotacao("CFOP Interestadual")]
        public virtual string CfopSaidaInterestadual { get => cfopSaidaInterestadual; set => cfopSaidaInterestadual = value; }
        [Anotacao("Alíq.ICMS")]
        public virtual string AliquotaIcms { get => aliquotaIcms; set => aliquotaIcms = value; }
        [Anotacao("CSOSN")]
        public virtual string CsosnSaida { get => csosnSaida; set => csosnSaida = value; }

        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }


        public override string ToString()
        {
            return descricao;
        }

    }
}
