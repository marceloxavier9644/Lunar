using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class FormaPagamentoController : Controller
    {
        public void salvarSeNaoExistir(FormaPagamento formaPagamento)
        {
            try
            {
                Conexao.IniciaTransacao();
                formaPagamento.DataCadastro = DateTime.Now;
                formaPagamento.OperadorCadastro = "1";
                FormaPagamentoBO bo = new FormaPagamentoBO();
                bo.salvarSeNaoExistir(formaPagamento);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + formaPagamento.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
