using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class BoletoConfigDAO : BaseDAO
    {
        public IList<BoletoConfig> selecionarTodosBoletosConfig()
        {
            Session = Conexao.GetSession();
            String sql = "FROM BoletoConfig as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<BoletoConfig> retorno = Session.CreateQuery(sql).List<BoletoConfig>();
            return retorno;
        }
        public IList<BoletoConfig> selecionarBoletoConfigPorContaBancaria(int idContaBancaria)
        {
            Session = Conexao.GetSession();
            String sql = "FROM BoletoConfig as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.ContaBancaria = " + idContaBancaria;
            IList<BoletoConfig> retorno = Session.CreateQuery(sql).List<BoletoConfig>();
            return retorno;
        }
        public BoletoConfig selecionarBoletoConfigPorContaBancariaUnica(ContaBancaria contaBancaria)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from BoletoConfig as Tabela where Tabela.ContaBancaria = " + contaBancaria.Id + " and Tabela.FlagExcluido <> true").UniqueResult<BoletoConfig>();
        }
    }
}
