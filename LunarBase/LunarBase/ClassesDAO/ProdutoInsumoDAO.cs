using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ProdutoInsumoDAO : BaseDAO
    {
       public IList<ProdutoInsumo> selecionarInsumoPorProduto(int idProduto)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoInsumo as Tabela WHERE Tabela.IdProdutoProduzido = '" + idProduto + "' and Tabela.FlagExcluido <> true";
            IList<ProdutoInsumo> retorno = Session.CreateQuery(sql).List<ProdutoInsumo>();
            return retorno;
        }
    }
}
