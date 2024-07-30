using LunarBase.Classes;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class AlertaClienteDAO : BaseDAO
    {
        public IList<AlertaCliente> selecionarAlertaPorPessoa(int idPessoa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM AlertaCliente as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Pessoa = " + idPessoa + " order by Tabela.DataCadastro Desc";
            IList<AlertaCliente> retorno = Session.CreateQuery(sql).List<AlertaCliente>();
            return retorno;
        }
    }
}
