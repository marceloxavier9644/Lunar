using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class AnexoDAO : BaseDAO
    {
        public IList<Anexo> selecionarTodosAnexosPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Anexo as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.OrdemServico = " + idOrdemServico + " order by Tabela.DataCadastro Desc";
            IList<Anexo> retorno = Session.CreateQuery(sql).List<Anexo>();
            return retorno;
        }
    }
}
