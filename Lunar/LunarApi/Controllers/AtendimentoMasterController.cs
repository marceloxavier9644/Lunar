using LunarBase.Classes;
using LunarBase.ClassesDTO;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class AtendimentoMasterControllerApi : ControllerBase
    {
        [HttpPost]
        [Route("SalvarAtendimentoMaster")]
        public async Task<IActionResult> SalvarAtendimentoMaster([FromBody] AtendimentoMasterDto atendimentoMasterDto)
        {
            try
            {
                NHibernateHelper.BeginTransaction();

                // Salvar Atendimento
                Atendimento atendimento = new Atendimento
                {
                    Data = atendimentoMasterDto.Atendimento.Data,
                    EmpresaFilial = 1,
                    Identificacao = atendimentoMasterDto.Atendimento.Identificacao,
                    Observacao = atendimentoMasterDto.Atendimento.Observacoes
                };
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimento);

                // Atualizar o ID do Atendimento na DTO
                atendimentoMasterDto.Atendimento.Id = atendimento.Id;

                // Carregar Mesa existente
                AtendimentoMesa mesa = new AtendimentoMesa { Id = atendimentoMasterDto.IdMesa };
                mesa = (AtendimentoMesa)LunarBase.ControllerBO.Controller.getInstance().selecionar(mesa);
                mesa.Status = "OCUPADO";
                mesa.AtendimentoId = atendimento.Id;
                LunarBase.ControllerBO.Controller.getInstance().salvar(mesa);

                // Salvar AtendimentoConta
                AtendimentoConta atendimentoConta = new AtendimentoConta
                {
                    AtendimentoId = atendimento.Id,
                    Cliente = null, // Ajuste conforme necessário
                    NomeCliente = atendimentoMasterDto.AtendimentoConta.NomeCliente
                };
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimentoConta);

                // Atualizar o ID da AtendimentoConta na DTO
                atendimentoMasterDto.AtendimentoConta.Id = atendimentoConta.Id;

                // Salvar AtendimentoVinculo
                AtendimentoVinculo atendimentoVinculo = new AtendimentoVinculo
                {
                    AtendimentoId = atendimento.Id,
                    ContaId = atendimentoConta.Id,
                    MesaId = mesa.Id,
                    OperadorCadastro = atendimentoMasterDto.AtendimentoVinculo.Operador
                };
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimentoVinculo);

                NHibernateHelper.CommitTransaction();

                // Retorne um objeto com os IDs gerados
                var response = new
                {
                    AtendimentoId = atendimento.Id,
                    AtendimentoContaId = atendimentoConta.Id,
                    MesaId = mesa.Id,
                    Message = "Abertura de Mesa Realizada com Sucesso!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                NHibernateHelper.RollbackTransaction();
                return BadRequest("Erro ao salvar atendimento: " + ex.Message);
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
    }
}
