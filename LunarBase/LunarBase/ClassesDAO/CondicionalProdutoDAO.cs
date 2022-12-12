using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class CondicionalProdutoDAO : BaseDAO
    {
        public IList<CondicionalProduto> selecionarProdutosPorCondicional(int idCondicional)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CondicionalProduto as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Condicional = " + idCondicional;
            IList<CondicionalProduto> retorno = Session.CreateQuery(sql).List<CondicionalProduto>();
            return retorno;
        }
        public IList<CondicionalProduto> selecionarProdutosCondicionalComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CondicionalProduto as Tabela WHERE CONCAT(Tabela.DescricaoProduto, ' ', Tabela.Produto.Id, ' ', Tabela.Produto.Ean, ' ', Tabela.Produto.Referencia, ' ', Tabela.Produto.Ncm) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.DescricaoProduto";
            IList<CondicionalProduto> retorno = Session.CreateQuery(sql).List<CondicionalProduto>();
            return retorno;
        }

    }
}
