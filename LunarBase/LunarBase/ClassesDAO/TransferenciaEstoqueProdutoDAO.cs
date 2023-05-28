using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class TransferenciaEstoqueProdutoDAO : BaseDAO
    {
        public IList<TransferenciaEstoqueProduto> selecionarProdutosPorTransferencia(int idTransferencia)
        {
            Session = Conexao.GetSession();
            String sql = "FROM TransferenciaEstoqueProduto as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.TransferenciaEstoque = " + idTransferencia;
            IList<TransferenciaEstoqueProduto> retorno = Session.CreateQuery(sql).List<TransferenciaEstoqueProduto>();
            return retorno;
        }
    }
}
