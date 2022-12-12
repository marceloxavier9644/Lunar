using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ProdutoSubGrupoDAO : BaseDAO
    {
        public IList<ProdutoSubGrupo> selecionarProdutoSubGrupoComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoSubGrupo as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " order by Tabela.Descricao";
            IList<ProdutoSubGrupo> retorno = Session.CreateQuery(sql).List<ProdutoSubGrupo>();
            return retorno;
        }
    }
}
