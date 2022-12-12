using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class NfeReferenciaDAO : BaseDAO
    {
        public IList<NfeReferencia> selecionarNotasReferenciadasPorNfe(int idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfeReferencia as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Nfe = " + idNfe;
            IList<NfeReferencia> retorno = Session.CreateQuery(sql).List<NfeReferencia>();
            return retorno;
        }
        public void excluirReferenciaNfeParaAtualizar(string idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "Update NfeReferencia as Tabela Set Tabela.FlagExcluido = true WHERE Tabela.Nfe = " + idNfe + " and Tabela.FlagExcluido <> true";
            Session.CreateQuery(sql).ExecuteUpdate();
        }
    }
}
