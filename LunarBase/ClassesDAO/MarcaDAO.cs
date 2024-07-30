using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class MarcaDAO : BaseDAO
    {
        public IList<Marca> selecionarMarcaComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Marca as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " order by Tabela.Descricao";
            IList<Marca> retorno = Session.CreateQuery(sql).List<Marca>();
            return retorno;
        }

        public Marca selecionarMarcaPorDescricao(string descricao)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Marca as Tabela where Tabela.Descricao = '" + descricao + "' and Tabela.FlagExcluido <> true").UniqueResult<Marca>();
        }
    }
}
