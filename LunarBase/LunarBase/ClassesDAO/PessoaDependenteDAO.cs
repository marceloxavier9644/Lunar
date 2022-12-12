using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class PessoaDependenteDAO : BaseDAO
    {
        public IList<PessoaDependente> selecionarDependentePorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaDependente as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = "+idPessoa+" order by Tabela.Nome";
            IList<PessoaDependente> retorno = Session.CreateQuery(sql).List<PessoaDependente>();
            return retorno;
        }
    }
}
