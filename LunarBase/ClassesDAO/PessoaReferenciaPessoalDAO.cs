using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class PessoaReferenciaPessoalDAO : BaseDAO
    {
        public IList<PessoaReferenciaPessoal> selecionarReferenciaPessoalPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaReferenciaPessoal as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<PessoaReferenciaPessoal> retorno = Session.CreateQuery(sql).List<PessoaReferenciaPessoal>();
            return retorno;
        }
    }
}
