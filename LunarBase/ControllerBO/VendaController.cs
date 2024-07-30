using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ControllerBO
{
    public class VendaController : Controller
    {
        public Venda selecionarVendaPorNF(int idNfe)
        {
            VendaBO bo = new VendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarVendaPorNF(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Venda> selecionarTop5VendaPorVendedores(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            VendaBO bo = new VendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTop5VendaPorVendedores(filial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Venda> selecionarVendaPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            VendaBO bo = new VendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarVendaPorPeriodo(filial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Venda> selecionarVendaPorSql(string sql)
        {
            VendaBO bo = new VendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarVendaPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
