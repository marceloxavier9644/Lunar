using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class UsuarioDAO : BaseDAO
    {
        public Usuario selecionarPorUsuarioESenha(Usuario usuario)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Usuario as Tabela where Tabela.Login = '" + usuario.Login + "' and Tabela.Senha = '" + usuario.Senha + "' and Tabela.FlagExcluido <> True").UniqueResult<Usuario>();
        }
        public IList<Usuario> selecionarTodosUsuarios()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Usuario as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Id <> 1";
            IList<Usuario> retorno = Session.CreateQuery(sql).List<Usuario>();
            return retorno;
        }
        public IList<Usuario> selecionarUsuarioComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Usuario as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Login) like '%" + valor + "%' and Tabela.FlagExcluido <> true and Tabela.Id <> 1 order by Tabela.Login";
            IList<Usuario> retorno = Session.CreateQuery(sql).List<Usuario>();
            return retorno;
        }

        public IList<Usuario> selecionarTodosUsuariosComNotificoes()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Usuario as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Id <> 1 and Tabela.Notificacoes = true";
            IList<Usuario> retorno = Session.CreateQuery(sql).List<Usuario>();
            return retorno;
        }
    }
}
