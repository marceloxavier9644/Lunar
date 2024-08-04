using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ProdutoCodigoBarrasDAO : BaseDAO
    {
        public IList<ProdutoCodigoBarras> selecionarCodigoBarrasPorSQL(string sql)
        {
            Session = Conexao.GetSession();
            IList<ProdutoCodigoBarras> retorno = Session.CreateSQLQuery(sql).AddEntity("f", typeof(ProdutoCodigoBarras)).List<ProdutoCodigoBarras>();
            return retorno;
        }
        public ProdutoCodigoBarras selecionarCodigoBarrasPorGrade(int idGrade)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from ProdutoCodigoBarras as Tabela where Tabela.ProdutoGrade = " + idGrade + " and Tabela.FlagExcluido <> true").UniqueResult<ProdutoCodigoBarras>();
        }
    }
}
