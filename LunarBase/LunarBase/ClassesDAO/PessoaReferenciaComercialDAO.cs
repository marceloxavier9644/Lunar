using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class PessoaReferenciaComercialDAO : BaseDAO
    {
        public IList<PessoaReferenciaComercial> selecionarReferenciaComercialPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaReferenciaComercial as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<PessoaReferenciaComercial> retorno = Session.CreateQuery(sql).List<PessoaReferenciaComercial>();
            return retorno;
        }
    }
}
