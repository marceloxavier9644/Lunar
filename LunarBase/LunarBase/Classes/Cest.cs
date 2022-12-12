using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("CEST")]
    public class Cest : ObjetoPadrao
    {
        private int id;
        private string numeroCest;
        private string ncm;
        private string segmento;
        private string item;
        private string descricaoCest;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("CEST")]
        public virtual string NumeroCest { get => numeroCest; set => numeroCest = value; }
        [Anotacao("NCM")]
        public virtual string Ncm { get => ncm; set => ncm = value; }
        [Anotacao("Descrição")]
        public virtual string DescricaoCest { get => descricaoCest; set => descricaoCest = value; }
        [Anotacao("Segmento")]
        public virtual string Segmento { get => segmento; set => segmento = value; }
        [Anotacao("Item")]
        [OcultarEmGridsEPesquisas]
        public virtual string Item { get => item; set => item = value; }

    }
}
