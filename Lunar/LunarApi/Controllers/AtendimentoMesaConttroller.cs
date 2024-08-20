using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api/Mesas")]
    public class AtendimentoMesaControllerApi : ControllerBase
    {
        // Método para obter todas as mesas e comandas com um nome específico de consulta
        [HttpGet("ListaMesas")]
        public async Task<IActionResult> GetAtendimentoMesas()
        {
            List<AtendimentoMesa> mesas = new List<AtendimentoMesa>();

            try
            {
                NHibernateHelper.OpenSession(); // Abra a sessão NHibernate
                NHibernateHelper.BeginTransaction(); // Inicie a transação, se necessário

                var controller = LunarBase.ControllerBO.Controller.getInstance(); // Instância do Controller do LunarBase

                IList<ObjetoPadrao> listaObjetos = controller.selecionarTodos(new AtendimentoMesa());

                foreach (var objeto in listaObjetos)
                {
                    mesas.Add((AtendimentoMesa)objeto);
                }

                NHibernateHelper.CommitTransaction(); // Commit da transação se estiver sendo usada

                return Ok(mesas);
            }
            catch (Exception ex)
            {
                NHibernateHelper.RollbackTransaction(); // Rollback da transação em caso de erro
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                NHibernateHelper.CloseSession(); // Feche a sessão NHibernate
            }
        }


        [HttpDelete("DeletarTodas")]
        public IActionResult DeleteAllMesas()
        {
            try
            {
                NHibernateHelper.OpenSession(); // Abra a sessão NHibernate
                NHibernateHelper.BeginTransaction(); // Inicie a transação

                var controller = LunarBase.ControllerBO.Controller.getInstance();
                AtendimentoMesaController atendimentoMesaController = new LunarBase.ControllerBO.AtendimentoMesaController();

                IList<AtendimentoMesa> listaObjetos = atendimentoMesaController.selecionarTodasMesas();
                foreach (AtendimentoMesa objeto in listaObjetos)
                {
                    controller.excluir(objeto);
                }

                NHibernateHelper.CommitTransaction(); // Commit da transação

                // Notificar WebSocket sobre a exclusão
                string message = "Todas as mesas foram excluídas.";
                WebSocketHandler.SendMessageToAllAsync(message).Wait(); // Envia uma mensagem de texto para todos os WebSockets

                return NoContent();
            }
            catch (Exception ex)
            {
                NHibernateHelper.RollbackTransaction(); // Rollback da transação em caso de erro
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                NHibernateHelper.CloseSession(); // Feche a sessão NHibernate
            }
        }

    }
}