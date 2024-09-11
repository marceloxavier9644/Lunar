using LunarBase.ClassesDAO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Telas.CaixaConferencia.ClassesAuxiliaresCaixa
{
    public class CaixaService
    {
        private CaixaDAO caixaDAO;

        public CaixaService()
        {
            CaixaDAO caixaDAO = new CaixaDAO();
            this.caixaDAO = caixaDAO;
        }

        public decimal ObterSaldo(DateTime dataLimite, int usuarioId)
        {
            string tipoCaixa = Sessao.parametroSistema.TipoCaixa;
            decimal saldo = 0;
            string sql;

            string dataFormatada = dataLimite.ToString("yyyy-MM-dd");

            if (tipoCaixa.Equals("INDIVIDUAL"))
            {
                sql = "SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo " +
                      "FROM Caixa Tabela WHERE Tabela.Usuario = " + usuarioId + " AND Tabela.FormaPagamento = 1 " +
                      "AND Tabela.FLAGEXCLUIDO <> true AND Tabela.DataLancamento <= '" + dataFormatada + " 23:59:59'";
            }
            else
            {
                sql = "SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo " +
                      "FROM Caixa Tabela WHERE Tabela.FormaPagamento = 1 " +
                      "AND Tabela.FLAGEXCLUIDO <> true AND Tabela.DataLancamento <= '" + dataFormatada + " 23:59:59'";

                saldo = caixaDAO.SelecionarSaldoPorSqlNativo(sql);
            }
            return saldo;
        }
    }

}
