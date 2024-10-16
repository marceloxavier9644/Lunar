using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class PessoaCaracteristicaDAO : BaseDAO
    {
        public IList<PessoaCaracteristica> selecionarTodasPessoaCaracteristica()
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaCaracteristica as Tabela WHERE Tabela.FlagExcluido <> true " +
                         "order by Tabela.Ordenacao";
            IList<PessoaCaracteristica> retorno = Session.CreateQuery(sql).List<PessoaCaracteristica>();
            return retorno;
        }
    }
}
