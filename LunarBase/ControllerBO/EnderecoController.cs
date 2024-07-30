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
    public class EnderecoController : Controller
    {
        public IList<Endereco> selecionarEnderecoPorPessoa(int idPessoa)
        {
            EnderecoBO bo = new EnderecoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEnderecoPorPessoa(idPessoa);
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
