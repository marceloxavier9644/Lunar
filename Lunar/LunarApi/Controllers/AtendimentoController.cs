using LunarBase.Classes;
using LunarBase.ClassesDTO;
using LunarBase.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api/Atendimento")]
    public class AtendimentoControllerApi : ControllerBase
    {
        [HttpPost]
        public IActionResult SalvarAtendimento([FromBody] AtendimentoDto atendimentoDto)
        {
            if (atendimentoDto == null)
            {
                return BadRequest("Atendimento é nulo");
            }
            Atendimento atendimento = new Atendimento();
            //atendimento.EmpresaFilial = Sessao.empresaFilialLogada.Id;
            atendimento.Identificacao = atendimentoDto.Identificacao;
            atendimento.Observacao = atendimentoDto.Observacoes;
            atendimento.Data = atendimentoDto.Data;
            LunarBase.ControllerBO.Controller.getInstance().salvar(atendimento);

            return Ok(atendimento.Id);
        }
    }
}
