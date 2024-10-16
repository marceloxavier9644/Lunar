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
    public class RetornoBancoController : Controller
    {
        public IList<RetornoBanco> selecionarRetornoPorPeriodo(string dataInicial, string dataFinal)
        {
            RetornoBancoBO bo = new RetornoBancoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarRetornoPorPeriodo(dataInicial, dataFinal);
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

        public RetornoBanco selecionarRetornoPorNossoNumero(string nossoNumero)
        {
            RetornoBancoBO bo = new RetornoBancoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarRetornoPorNossoNumero(nossoNumero);
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
