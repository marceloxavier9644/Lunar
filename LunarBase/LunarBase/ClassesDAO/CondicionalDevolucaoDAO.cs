using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class CondicionalDevolucaoDAO : BaseDAO
    {
        public IList<CondicionalDevolucao> selecionarProdutosDevolvidosPorCondicional(int idCondicional)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CondicionalDevolucao as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Condicional = " + idCondicional;
            IList<CondicionalDevolucao> retorno = Session.CreateQuery(sql).List<CondicionalDevolucao>();
            return retorno;
        }

        public CondicionalDevolucao selecionarProdutoDevolvido(int idCondicional, int idProduto)
        {
            //Feito para validar devolucoes no momento da devolucao
            Session = Conexao.GetSession();
            String sql = "FROM CondicionalDevolucao as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Condicional = " + idCondicional + " and Tabela.Produto = " + idProduto;
            return Session.CreateQuery(sql).UniqueResult<CondicionalDevolucao>();
        }
        public double calcularQuantidadeProdutoDevolvido(int idCondicional, int idProduto)
        {
            string sql = "select sum(tabela.Quantidade) as QuantidadeDevolvido from CondicionalDevolucao tabela where tabela.Produto = " + idProduto + " and tabela.Condicional = " + idCondicional + " and tabela.FLAGEXCLUIDO <> true group by tabela.PRODUTO";
            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }
    }
}
