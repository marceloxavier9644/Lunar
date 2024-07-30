using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ProdutoSetorDAO : BaseDAO
    {
        public IList<ProdutoSetor> selecionarProdutoSetorComVariosFiltros(string valor, EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoSetor as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " and Tabela.EmpresaFilial = "+empresaLogada.Id+" order by Tabela.Descricao";
            IList<ProdutoSetor> retorno = Session.CreateQuery(sql).List<ProdutoSetor>();
            return retorno;
        }
    }
}
