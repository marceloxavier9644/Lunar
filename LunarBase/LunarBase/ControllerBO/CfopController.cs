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
    public class CfopController : Controller
    {
        public IList<Cfop> selecionarTodosCfop()
        {
            CfopBO bo = new CfopBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCfop();
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

        public IList<Cfop> selecionarCfopPorCfop(String cfop)
        {
            CfopBO bo = new CfopBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCfopPorCfop(cfop);
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
