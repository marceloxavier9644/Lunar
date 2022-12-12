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
    public class PessoaReferenciaPessoalController : Controller
    {
        public IList<PessoaReferenciaPessoal> selecionarReferenciaPessoalPorPessoa(int idPessoa)
        {
            PessoaReferenciaPessoalBO bo = new PessoaReferenciaPessoalBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarReferenciaPessoalPorPessoa(idPessoa);
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
