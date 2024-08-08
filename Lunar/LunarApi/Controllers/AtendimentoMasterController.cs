using LunarBase.Classes;
using LunarBase.ClassesDTO;
using Microsoft.AspNetCore.Mvc;

namespace LunarApi.Controllers
{
    public class AtendimentoMasterControllerApi : Controller
    {
        [HttpPost]
        [Route("api/AtendimentoMaster")]
        public async Task<IActionResult> SalvarAtendimentoMaster([FromBody] AtendimentoMasterDto atendimentoMasterDto)
        {
            try
            {
                NHibernateHelper.BeginTransaction();

                // Carregar Mesa existente
                AtendimentoMesa mesa = new AtendimentoMesa { Id = atendimentoMasterDto.IdMesa };
                mesa = (AtendimentoMesa)LunarBase.ControllerBO.Controller.getInstance().selecionar(mesa);
                mesa.Status = "OCUPADO";
                LunarBase.ControllerBO.Controller.getInstance().salvar(mesa);

                // Salvar Atendimento
                Atendimento atendimento = new Atendimento
                {
                    Data = atendimentoMasterDto.Atendimento.Data,
                    EmpresaFilial = 1,
                    Identificacao = atendimentoMasterDto.Atendimento.Identificacao,
                    Observacao = atendimentoMasterDto.Atendimento.Observacoes
                };
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimento);

                // Atualizar o ID do Atendimento na entidade
                atendimentoMasterDto.Atendimento.Id = atendimento.Id;

                // Salvar AtendimentoConta
                AtendimentoConta atendimentoConta = new AtendimentoConta
                {
                    AtendimentoId = atendimento.Id,
                    Cliente = null,
                    NomeCliente = atendimentoMasterDto.AtendimentoConta.NomeCliente
                };
                LunarBase.ControllerBO.Controller.getInstance().salvar(atendimentoConta);

                // Atualizar o ID da AtendimentoConta na entidade
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

                return Ok("Abertura de Mesa Realizada com Sucesso!");
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