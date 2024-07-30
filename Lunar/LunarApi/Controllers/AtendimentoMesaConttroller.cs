using LunarBase.Classes;
using LunarBase.ControllerBO;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api/Mesas")]
    public class AtendimentoMesaControllerApi : ControllerBase
    {
        // Método para obter todas as mesas e comandas com um nome específico de consulta
        [HttpGet("ListaMesas")]
        public ActionResult<List<AtendimentoMesa>> GetAtendimentoMesas()
        {
            List<AtendimentoMesa> mesas = new List<AtendimentoMesa>();
            var controller = LunarBase.ControllerBO.Controller.getInstance(); // Instância do Controller do LunarBase
            try
            {
                IList<ObjetoPadrao> listaObjetos = controller.selecionarTodos(new AtendimentoMesa());
                foreach (var objeto in listaObjetos)
                {
                    mesas.Add((AtendimentoMesa)objeto);
                }
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // Método para deletar todas as mesas
        [HttpDelete("DeletarTodas")]
        public IActionResult DeleteAllMesas()
        {
            var controller = LunarBase.ControllerBO.Controller.getInstance(); 
            try
            {
                AtendimentoMesaController atendimentoMesaController = new LunarBase.ControllerBO.AtendimentoMesaController();
                IList<AtendimentoMesa> listaObjetos = atendimentoMesaController.selecionarTodasMesas();
                foreach (AtendimentoMesa objeto in listaObjetos)
                {
                    controller.excluir(objeto);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
