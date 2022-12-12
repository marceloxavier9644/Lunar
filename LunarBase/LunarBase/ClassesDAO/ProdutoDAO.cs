using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ProdutoDAO : BaseDAO
    {
        public Produto selecionarProdutoPorCodigoUnicoEFilial(int idProduto, EmpresaFilial empresaFilial)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Produto as Tabela where Tabela.Id = '" + idProduto + "' and Tabela.EmpresaFilial = " + empresaFilial.Id + " and Tabela.FlagExcluido <> true").UniqueResult<Produto>();
        }

        public IList<Produto> selecionarTodosProdutorPorFilial(EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.EmpresaFilial = " + empresaLogada.Id + " order by Tabela.Descricao"; 
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarProdutosComVariosFiltros(string valor, EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.EmpresaFilial = " + empresaLogada.Id + " order by Tabela.Descricao";
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarProdutosPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarProdutoPorCodigoBarras(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE Tabela.Ean = '" + valor + "' and Tabela.FlagExcluido <> true order by Tabela.Descricao";
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }
    }
}
