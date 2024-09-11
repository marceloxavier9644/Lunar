using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CaixaAberturaDAO : BaseDAO
    {
        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuario(int idUsuario)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CaixaAbertura Tabela WHERE Tabela.Usuario = " + idUsuario + " and Tabela.Status = 'ABERTO' and Tabela.FlagExcluido <> true";
            IList<CaixaAbertura> retorno = Session.CreateQuery(sql).List<CaixaAbertura>();
            return retorno;
        }

        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuarioEData(int idUsuario, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CaixaAbertura Tabela WHERE Tabela.Usuario = " + idUsuario + " and " +
                "Tabela.DataAbertura between '"+dataInicial+" 00:00:00' and '" + dataFinal + " 23:59:59' and Tabela.Status = 'ABERTO' and Tabela.FlagExcluido <> true";
            IList<CaixaAbertura> retorno = Session.CreateQuery(sql).List<CaixaAbertura>();
            return retorno;
        }
        public IList<CaixaAbertura> selecionarTodosCaixasAbertos()
        {
            Session = Conexao.GetSession();
            String sql = "FROM CaixaAbertura Tabela WHERE Tabela.Status = 'ABERTO' and Tabela.FlagExcluido <> true";
            IList<CaixaAbertura> retorno = Session.CreateQuery(sql).List<CaixaAbertura>();
            return retorno;
        }

    }
}
