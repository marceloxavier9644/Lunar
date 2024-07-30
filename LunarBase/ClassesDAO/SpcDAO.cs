using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class SpcDAO : BaseDAO
    {
        public IList<Spc> selecionarRegistrosPorCliente(Pessoa pessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Spc as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = "+ pessoa.Id+ " order by Tabela.DataConsulta";
            IList<Spc> retorno = Session.CreateQuery(sql).List<Spc>();
            return retorno;
        }
    }
}
