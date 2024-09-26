using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class NfseDAO : BaseDAO
    {
        public IList<Nfse> selecionarNFSePorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfse as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.OrdemServico = " + idOrdemServico;
            IList<Nfse> retorno = Session.CreateQuery(sql).List<Nfse>();
            return retorno;
        }
    }
}
