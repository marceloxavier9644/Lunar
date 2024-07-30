using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class NfeCceDAO : BaseDAO
    {
        public IList<NfeCce> selecionarCartaCorrecaoPorNfe(int idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfeCce as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Nfe = " + idNfe;                     
            IList<NfeCce> retorno = Session.CreateQuery(sql).List<NfeCce>();
            return retorno;
        }
    }
}
