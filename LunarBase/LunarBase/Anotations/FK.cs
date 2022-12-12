namespace LunarBase.Anotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class FK : Attribute
    {
        private String vDescricao;

        public virtual String Descricao
        {
            get { return vDescricao; }
        }

        public FK(String Descricao)
        {
            vDescricao = Descricao;
        }

        public override string ToString()
        {
            return vDescricao;
        }
    }
}
