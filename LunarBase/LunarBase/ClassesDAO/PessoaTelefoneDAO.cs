using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class PessoaTelefoneDAO : BaseDAO
    {
        public IList<PessoaTelefone> selecionarTelefonePorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PessoaTelefone as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa;
            IList<PessoaTelefone> retorno = Session.CreateQuery(sql).List<PessoaTelefone>();
            return retorno;
        }
    }
}
