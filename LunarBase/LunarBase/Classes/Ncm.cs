using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("NCM")]
    public class Ncm : ObjetoPadrao
    {
        private int id;
        private string numeroNcm;
        private string descricaoNcm;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("NCM")]
        public virtual string NumeroNcm { get => numeroNcm; set => numeroNcm = value; }
        [Anotacao("Descrição")]
        public virtual string DescricaoNcm { get => descricaoNcm; set => descricaoNcm = value; }
        public override string ToString()
        {
            return numeroNcm;
        }
    }
}
