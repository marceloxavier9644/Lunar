using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class EmpresaDAO : BaseDAO
    {
        public Empresa selecionarEmpresaPorCPFCNPJ(string cpfCNPJ)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Empresa as Tabela where Tabela.Cnpj = '" + cpfCNPJ + "' and Tabela.FlagExcluido <> true").UniqueResult<Empresa>();
        }

        public Empresa selecionarEmpresaPorId(int id)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Empresa as Tabela where Tabela.Id = "+id+" and Tabela.FlagExcluido <> true").UniqueResult<Empresa>();
        }

        public IList<Empresa> selecionarTodasEmpresas()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Empresa as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Empresa> retorno = Session.CreateQuery(sql).List<Empresa>();
            return retorno;
        }
    }
}
