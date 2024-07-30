using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ServicoDAO : BaseDAO
    {
        public IList<Servico> selecionarTodosServico()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Servico as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Servico> retorno = Session.CreateQuery(sql).List<Servico>();
            return retorno;
        }

        public IList<Servico> selecionarServicoComVariosFiltros(string valor, EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Servico as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.Filial = " + empresaLogada.Id + " order by Tabela.Descricao";
            IList<Servico> retorno = Session.CreateQuery(sql).List<Servico>();
            return retorno;
        }
    }
}
