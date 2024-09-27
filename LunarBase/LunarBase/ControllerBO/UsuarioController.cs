using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class UsuarioController : Controller
    {
        public void salvarSeNaoExistir(Usuario usuario)
        {
            try
            {
                Conexao.IniciaTransacao();
                usuario.DataCadastro = DateTime.Now;
                usuario.OperadorCadastro = "1";
                UsuarioBO bo = new UsuarioBO();
                bo.salvarSeNaoExistir(usuario);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + usuario.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public Usuario selecionarUsuarioPorUsuarioESenha(Usuario usuario)
        {
            UsuarioBO bo = new UsuarioBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUsuarioPorUsuarioESenha(usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Usuario> selecionarTodosUsuarios()
        {
            UsuarioBO bo = new UsuarioBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosUsuarios();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Usuario> selecionarUsuarioComVariosFiltros(string valor)
        {
            UsuarioBO bo = new UsuarioBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUsuarioComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Usuario> selecionarTodosUsuariosComNotificoes()
        {
            UsuarioBO bo = new UsuarioBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosUsuariosComNotificoes();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
