using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ContaBancariaDAO : BaseDAO
    {
        public IList<ContaBancaria> selecionarTodasContas()
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaBancaria as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<ContaBancaria> retorno = Session.CreateQuery(sql).List<ContaBancaria>();
            return retorno;
        }

        public IList<ContaBancaria> selecionarTodasContasPorFilial(int idFilial)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaBancaria as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.EmpresaFilial = " + idFilial;
            IList<ContaBancaria> retorno = Session.CreateQuery(sql).List<ContaBancaria>();
            return retorno;
        }

        public IList<ContaBancaria> selecionarContaBancariaComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaBancaria as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Conta, ' ', Tabela.Agencia) like '%" + valor + "%' and Tabela.FlagExcluido <> true";
            IList<ContaBancaria> retorno = Session.CreateQuery(sql).List<ContaBancaria>();
            return retorno;
        }
    }
}
