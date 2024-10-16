using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class RetornoBancoDAO : BaseDAO
    {
        public IList<RetornoBanco> selecionarRetornoPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            string sql = "FROM RetornoBanco Tabela " +
                         "WHERE Tabela.DataPagamento between '" + dataInicial + "' and '" + dataFinal + "'";

            IQuery query = Session.CreateQuery(sql);
            return query.List<RetornoBanco>();
        }

        public RetornoBanco selecionarRetornoPorNossoNumero(string nossoNumero)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from RetornoBanco as Tabela where Tabela.NossoNumero = '" + nossoNumero + "' and Tabela.FlagExcluido <> True").UniqueResult<RetornoBanco>();
        }
    }
}
