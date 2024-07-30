using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class NfePagamentoDAO : BaseDAO
    {
        public IList<NfePagamento> selecionarPagamentoPorNfe(int idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfePagamento as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Nfe = " + idNfe;
            IList<NfePagamento> retorno = Session.CreateQuery(sql).List<NfePagamento>();
            return retorno;
        }
    }
}
