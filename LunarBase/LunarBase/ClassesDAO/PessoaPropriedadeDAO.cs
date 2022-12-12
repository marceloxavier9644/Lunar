using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class PessoaPropriedadeDAO : BaseDAO
    {
        public IList<PessoaPropriedade> selecionarPropriedadesPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaPropriedade as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<PessoaPropriedade> retorno = Session.CreateQuery(sql).List<PessoaPropriedade>();
            return retorno;
        }
    }
}
