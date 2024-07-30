using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class EnderecoDAO : BaseDAO
    {
        public IList<Endereco> selecionarEnderecoPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Endereco as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<Endereco> retorno = Session.CreateQuery(sql).List<Endereco>();
            return retorno;
        }
    }
}
