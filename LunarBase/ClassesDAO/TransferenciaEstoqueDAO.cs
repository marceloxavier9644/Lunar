using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class TransferenciaEstoqueDAO : BaseDAO
    {
        public IList<TransferenciaEstoque> selecionarTodasTransferencias()
        {
            Session = Conexao.GetSession();
            String sql = "FROM TransferenciaEstoque as Tabela WHERE Tabela.FlagExcluido <> true order by Tabela.Data desc";
            IList<TransferenciaEstoque> retorno = Session.CreateQuery(sql).List<TransferenciaEstoque>();
            return retorno;
        }
    }
}
