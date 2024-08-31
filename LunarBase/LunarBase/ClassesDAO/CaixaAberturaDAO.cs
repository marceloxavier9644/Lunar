using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class CaixaAberturaDAO : BaseDAO
    {
        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuario(int idUsuario)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CaixaAbertura Tabela WHERE Tabela.Usuario = " + idUsuario + " and Tabela.FlagExcluido <> true";
            IList<CaixaAbertura> retorno = Session.CreateQuery(sql).List<CaixaAbertura>();
            return retorno;
        }

        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuarioEData(int idUsuario, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CaixaAbertura Tabela WHERE Tabela.Usuario = " + idUsuario + " and " +
                "Tabela.DataAbertura between '"+dataInicial+" 00:00:00' and '" + dataFinal + " 23:59:59'  and Tabela.FlagExcluido <> true";
            IList<CaixaAbertura> retorno = Session.CreateQuery(sql).List<CaixaAbertura>();
            return retorno;
        }
    }
}
