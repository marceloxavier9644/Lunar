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
    public class PessoaReferenciaComercialController : Controller
    {
        public IList<PessoaReferenciaComercial> selecionarReferenciaComercialPorPessoa(int idPessoa)
        {
            PessoaReferenciaComercialBO bo = new PessoaReferenciaComercialBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarReferenciaComercialPorPessoa(idPessoa);
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
