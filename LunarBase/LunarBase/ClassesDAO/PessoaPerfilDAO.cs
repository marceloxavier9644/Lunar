using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class PessoaPerfilDAO : BaseDAO
    {
        public IList<PessoaPerfil> selecionarPerfilPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaPerfil as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<PessoaPerfil> retorno = Session.CreateQuery(sql).List<PessoaPerfil>();
            return retorno;
        }
    }
}
