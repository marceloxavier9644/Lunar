using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CaracteristicaDAO : BaseDAO
    {
        public IList<Caracteristica> selecionarCaracteristicaPorProduto(int idProduto)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Caracteristica as Tabela WHERE Tabela.Produto = '" + idProduto + "' and Tabela.FlagExcluido <> true";
            IList<Caracteristica> retorno = Session.CreateQuery(sql).List<Caracteristica>();
            return retorno;
        }
    }
}
