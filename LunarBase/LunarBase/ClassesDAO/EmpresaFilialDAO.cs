using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class EmpresaFilialDAO : BaseDAO
    {
        public EmpresaFilial selecionarEmpresaFilialPorCPFCNPJ(string cpfCNPJ)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from EmpresaFilial as Tabela where Tabela.Cnpj = '" + cpfCNPJ + "' and Tabela.FlagExcluido <> true").UniqueResult<EmpresaFilial>();
        }

        public IList<EmpresaFilial> selecionarTodasFiliais()
        {
            Session = Conexao.GetSession();
            String sql = "FROM EmpresaFilial as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<EmpresaFilial> retorno = Session.CreateQuery(sql).List<EmpresaFilial>();
            return retorno;
        }
        public IList<EmpresaFilial> selecionarEmpresaFilialComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM EmpresaFilial as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + valor + "%' and Tabela.FlagExcluido <> true order by Tabela.RazaoSocial";
            IList<EmpresaFilial> retorno = Session.CreateQuery(sql).List<EmpresaFilial>();
            return retorno;
        }
    }
}
