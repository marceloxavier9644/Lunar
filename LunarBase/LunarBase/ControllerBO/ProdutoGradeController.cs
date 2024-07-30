using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ControllerBO
{
    public class ProdutoGradeController : Controller
    {
        public IList<ProdutoGrade> selecionarGradePorProduto(int idProduto)
        {
            ProdutoGradeBO bo = new ProdutoGradeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarGradePorProduto(idProduto);
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
