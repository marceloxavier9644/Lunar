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
    public class NcmController : Controller
    {
        public IList<Ncm> selecionarTodosNCM()
        {
            NcmBO bo = new NcmBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosNCM();
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

        public IList<Ncm> selecionarNCMPorNCM(String ncm)
        {
            NcmBO bo = new NcmBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNCMPorNCM(ncm);
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
