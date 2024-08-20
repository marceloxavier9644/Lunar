using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class AtendimentoContaDAO : BaseDAO
    {
        public IList<AtendimentoConta> selecionarTodosAtendimentoConta(int idAtendimento)
        {
            Session = Conexao.GetSession();
            string sql = "From AtendimentoConta as Tabela Where Tabela.AtendimentoId = '" + idAtendimento + "' and Tabela.FlagExcluido <> true";
            IList<AtendimentoConta> retorno = Session.CreateQuery(sql).List<AtendimentoConta>();
            return retorno;
        }
    }
}
