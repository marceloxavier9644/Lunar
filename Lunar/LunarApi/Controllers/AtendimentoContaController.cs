using LunarBase.Classes;
using LunarBase.ClassesDTO;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api/AtendimentoConta")]
    public class AtendimentoContaControllerApi : ControllerBase
    {
        [HttpPost]
        public IActionResult SalvarAtendimentoConta([FromBody] AtendimentoContaDto atendimentoContaDto)
        {
            if (atendimentoContaDto == null)
            {
                return BadRequest("Atendimento Conta DTO é nulo");
            }

            // Variável para armazenar o cliente, pode ser null
            Pessoa cliente = null;
            if (atendimentoContaDto.IdCliente.HasValue)
            {
                cliente.Id = atendimentoContaDto.IdCliente.Value;
                cliente = (Pessoa)LunarBase.ControllerBO.Controller.getInstance().selecionar(cliente);
            }

            AtendimentoConta atendimentoConta = new AtendimentoConta();
            atendimentoConta.AtendimentoId = atendimentoContaDto.IdAtendimento;
            atendimentoConta.Cliente = cliente;
            atendimentoConta.OperadorCadastro = "";
            atendimentoConta.NomeCliente = atendimentoContaDto.NomeCliente;

            try
            {
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimentoConta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar AtendimentoConta: {ex.Message}");
            }

            return Ok(atendimentoConta.Id);
        }
    }
}
