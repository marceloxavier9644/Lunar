using LunarBase.Anotations;
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
    public class CsosnController : Controller
    {
        public void salvarSeNaoExistir(Csosn csosn)
        {
            try
            {
                Conexao.IniciaTransacao();
                csosn.DataCadastro = DateTime.Now;
                csosn.OperadorCadastro = "1";
                CsosnBO bo = new CsosnBO();
                bo.salvarSeNaoExistir(csosn);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + csosn.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<Csosn> selecionarTodosCSOSN()
        {
            CsosnBO bo = new CsosnBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCSOSN();
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

        public IList<Csosn> selecionarCsosnPorCsosn(string csosn)
        {
            CsosnBO bo = new CsosnBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCsosnPorCsosn(csosn);
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
