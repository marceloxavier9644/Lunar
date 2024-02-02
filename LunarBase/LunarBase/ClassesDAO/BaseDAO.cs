using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;

namespace LunarBase.ClassesDAO
{
    public class BaseDAO
    {
        public ISession Session;

        public virtual void incluir(ObjetoPadrao objeto)
        {
            Session = Conexao.GetSession();
            ObjetoPadrao generico = (ObjetoPadrao)Type.GetType("LunarBase.Classes." + objeto.GetType().Name).GetConstructor(System.Type.EmptyTypes).Invoke(null);
            generico = objeto;
            Session.Save(generico);
        }

        public void alterar(ObjetoPadrao objeto)
        {
            Session = Conexao.GetSession();
            ObjetoPadrao generico = (ObjetoPadrao)Type.GetType("LunarBase.Classes." + objeto.GetType().Name).GetConstructor(System.Type.EmptyTypes).Invoke(null);
            generico = objeto;
            Session.Merge(generico);
        }

        public ObjetoPadrao Selecionar(ObjetoPadrao objeto, object id)
        {
            Session = Conexao.GetSession();
            objeto = (ObjetoPadrao)Session.Get("LunarBase.Classes." + objeto.GetType().Name, id);
            return objeto;
        }


        public IList<ObjetoPadrao> SelecionarTodos(String Tabela)
        {
            Session = Conexao.GetSession();
            String Sql = "from " + Tabela + " as tabela WHERE tabela.FlagExcluido <> true";
            IList<ObjetoPadrao> Resultado = Session.CreateQuery(Sql).List<ObjetoPadrao>();

            return Resultado;
        }
    }
}
