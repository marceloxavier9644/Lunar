using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ParcelamentoDAO : BaseDAO
    {
        public IList<Parcelamento> selecionarTodasCondicoesCredito()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Parcelamento as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Credito = true";
            IList<Parcelamento> retorno = Session.CreateQuery(sql).List<Parcelamento>();
            return retorno;
        }

        public IList<Parcelamento> selecionarTodasCondicoesDebito()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Parcelamento as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Debito = true";
            IList<Parcelamento> retorno = Session.CreateQuery(sql).List<Parcelamento>();
            return retorno;
        }
    }
}
