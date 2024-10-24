﻿using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CreditoClienteDAO : BaseDAO
    {
        public IList<CreditoCliente> selecionarCreditoPorCliente(int idCliente)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CreditoCliente as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Cliente = " + idCliente + " order by Tabela.DataCadastro";
            IList<CreditoCliente> retorno = Session.CreateQuery(sql).List<CreditoCliente>();
            return retorno;
        }
    }
}
