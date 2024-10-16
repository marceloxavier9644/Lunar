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
    public class PessoaCaracteristicaController : Controller
    {
        public IList<PessoaCaracteristica> selecionarTodasPessoaCaracteristica()
        {
            PessoaCaracteristicaBO bo = new PessoaCaracteristicaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasPessoaCaracteristica();
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
