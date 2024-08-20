using LunarBase.Classes;
using LunarBase.ClassesDTO;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api/AtendimentoVinculo")]
    public class AtendimentoVinculoControllerApi : Controller
    {
        [HttpPost]
        public IActionResult SalvarAtendimentoVinculo([FromBody] AtendimentoVinculoDto atendimentoVinculoDto)
        {
            if (atendimentoVinculoDto == null)
            {
                return BadRequest("Atendimento Vinculo DTO é nulo");
            }

            AtendimentoVinculo atendimentoVinculo = new AtendimentoVinculo();
            atendimentoVinculo.FlagExcluido = false;
            atendimentoVinculo.AtendimentoId = atendimentoVinculoDto.IdAtendimento;
            atendimentoVinculo.ContaId = atendimentoVinculoDto.IdConta;
            atendimentoVinculo.MesaId = atendimentoVinculoDto.IdMesa;
            atendimentoVinculo.OperadorCadastro = atendimentoVinculoDto.Operador;

            try
            {
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimentoVinculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar AtendimentoConta: {ex.Message}");
            }

            return Ok(atendimentoVinculo.Id);
        }

        [HttpGet("{idAtendimento}")]
        public IActionResult GetVinculosPorAtendimento(int idAtendimento)
        {
            try
            {
                var controller = LunarBase.ControllerBO.Controller.getInstance();
                var atendimentoVinculoController = new LunarBase.ControllerBO.AtendimentoVinculoController();

                IList<AtendimentoVinculo> listaVinculos = atendimentoVinculoController.selecionarVinculosPorAtendimento(idAtendimento);

                var vinculosDto = listaVinculos.Select(v => new AtendimentoVinculoDto
                {
                    Id = v.Id,
                    IdAtendimento = v.AtendimentoId,
                    IdConta = v.ContaId,
                    IdMesa = v.MesaId,
                    Operador = v.OperadorCadastro
                }).ToList();

                return Ok(vinculosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

