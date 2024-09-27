using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.ZAPZAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.ClassesRepeticoes
{
    public class NotificacaoService
    {
        private UsuarioController _usuarioController;
        private LunarChatAPI lunarChatAPI = new LunarChatAPI();

        public NotificacaoService()
        {
            _usuarioController = new UsuarioController();
        }

        // Método assíncrono para enviar notificações
        public async Task EnviarNotificacaoAsync(string mensagem, string tipoNotificacao)
        {
            IList<Usuario> listaUsuariosNotificacao = _usuarioController.selecionarTodosUsuariosComNotificoes();

            if (listaUsuariosNotificacao != null && listaUsuariosNotificacao.Count > 0)
            {
                // Executa a operação em segundo plano para evitar congelamento da UI
                await Task.Run(() =>
                {
                    foreach (Usuario user in listaUsuariosNotificacao)
                    {
                        if (!string.IsNullOrEmpty(user.NotificacoesSelecionadas))
                        {
                            string[] notificacoes = user.NotificacoesSelecionadas.Split(';');

                            foreach (string notificacao in notificacoes)
                            {
                                if (!string.IsNullOrWhiteSpace(notificacao))
                                {
                                    // Verifica se o tipo de notificação que o usuário deve receber corresponde ao tipoNotificacao
                                    if (notificacao.Equals(tipoNotificacao))
                                    {
                                        EnviarMensagemWhatsApp(user, mensagem);
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }

        // Método para enviar mensagens via WhatsApp
        private async void EnviarMensagemWhatsApp(Usuario user, string mensagem)
        {
            if (!string.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
            {
                string numeroTelefone = "55" + user.Ddd + user.Fone;
                await lunarChatAPI.SendMessageAsync(numeroTelefone, mensagem);
            }
        }

        // Método assíncrono para notificar a abertura de Ordem de Serviço
        public async Task NotificarAberturaOrdemServicoAsync(OrdemServico ordemServico)
        {
            String mensagem = "*📋 " + Sessao.empresaFilialLogada.NomeFantasia + "* \n\n"
                + "🛠 *Ordem de Serviço Aberta!* \n\n"
                + "Usuário: *" + Sessao.usuarioLogado.Login + "* \n"
                + "Cliente: *" + ordemServico.Cliente.RazaoSocial + "* \n"
                + "📅 *Data de Abertura:* " + ordemServico.DataAbertura.ToString("dd/MM/yyyy HH:mm:ss") + "\n"
                + "🔖 *Número da OS:* " + ordemServico.Id + "\n\n"
                + "*Observações:* \n" + ordemServico.Observacoes + "\n\n"
                + "🚀🚀🚀";

            // Chama o método assíncrono para enviar a notificação
            await EnviarNotificacaoAsync(mensagem, "1"); // "1" é o tipo de notificação para 'Ordem de Serviço Aberta'
        }
        public async Task NotificarEncerramentoOrdemServicoAsync(OrdemServico ordemServico)
        {
            String mensagem = "*📋 " + Sessao.empresaFilialLogada.NomeFantasia + "* \n\n"
                + "🛠 *Ordem de Serviço Encerrada!* \n\n"
                + "Cliente: *" + ordemServico.Cliente.RazaoSocial + "* \n"
                + "Usuário: *" + Sessao.usuarioLogado.Login + "* \n"
                + "📅 *Data de Abertura:* " + ordemServico.DataAbertura.ToString("dd/MM/yyyy HH:mm:ss") + "\n"
                + "📅 *Data de Encerramento:* " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n"
                + "🔖 *Número da OS:* " + ordemServico.Id + "\n\n"
                + "*Resumo Financeiro:* \n"
                + "💲 *Valor Produtos:* " + ordemServico.ValorProduto.ToString("C") + "\n"
                + "💲 *Valor Serviços:* " + ordemServico.ValorServico.ToString("C") + "\n"
                + "💲 *Desconto:* " + ordemServico.ValorDesconto.ToString("C") + "\n"
                + "💲 *Valor Total (com desconto):* " + ordemServico.ValorTotal.ToString("C") + "\n\n"
                + "🚀🚀🚀";

            // Chama o método assíncrono para enviar a notificação
            await EnviarNotificacaoAsync(mensagem, "2"); // "2" é o tipo de notificação para 'Ordem de Serviço Encerrada'
        }

        public async Task NotificarVendaRealizadaAsync(Venda venda)
        {
            // Verifica se o cliente é nulo e define uma mensagem padrão se for
            string nomeCliente = venda.Cliente != null ? venda.Cliente.RazaoSocial : "Cliente não informado";

            String mensagem = "*📋 " + Sessao.empresaFilialLogada.NomeFantasia + "* \n\n"
                + "🛒 *Venda Realizada!* \n\n"
                + "Cliente: *" + nomeCliente + "* \n"
                + "📅 *Data da Venda:* " + venda.DataVenda.ToString("dd/MM/yyyy HH:mm:ss") + "\n"
                + "🔖 *Número da Venda:* " + venda.Id + "\n\n"
                + "*Resumo Financeiro:* \n"
                + "💲 *Valor Produtos:* " + venda.ValorProdutos.ToString("C") + "\n"
                + "💲 *Desconto:* " + venda.ValorDesconto.ToString("C") + "\n"
                + "💲 *Valor Total (com desconto):* " + venda.ValorFinal.ToString("C") + "\n\n"
                + "🚀🚀🚀";

            // Chama o método assíncrono para enviar a notificação
            await EnviarNotificacaoAsync(mensagem, "3"); // "3" é o tipo de notificação para 'Venda Realizada'
        }

        public async Task NotificarContasAPagarAsync(IList<ContaPagar> listaPagar)
        {
            // Verifica se a lista de contas a pagar está vazia
            if (listaPagar == null || listaPagar.Count == 0)
            {
                await EnviarNotificacaoAsync("*📋 " + Sessao.empresaFilialLogada.NomeFantasia + "* \n\n"
                    + "🧾 *Não há contas a pagar para hoje ou já foram pagas!* \n\n"
                    + "🚀🚀🚀", "4"); // "4" é o tipo de notificação para 'Contas a Pagar Vazio'
                return;
            }

            StringBuilder mensagem = new StringBuilder();
            mensagem.AppendLine("*📋 " + Sessao.empresaFilialLogada.NomeFantasia + "* \n\n");
            mensagem.AppendLine("🧾 *Contas a Pagar do Dia* \n");

            // Loop para adicionar cada conta a pagar à mensagem
            foreach (var conta in listaPagar)
            {
                string nomeLocal = conta.Pessoa != null ? conta.Pessoa.RazaoSocial : "Local não informado";
                mensagem.AppendLine($"🔖 *Local:* {nomeLocal} \n" +
                                    $"📅 *Data de Vencimento:* {conta.DVenc.ToString("dd/MM/yyyy")} \n" +
                                    $"💲 *Valor:* {conta.VDup.ToString("C")} \n");
            }

            mensagem.AppendLine("🚀🚀🚀");

            // Chama o método assíncrono para enviar a notificação
            await EnviarNotificacaoAsync(mensagem.ToString(), "4"); // "4" é o tipo de notificação para 'Contas a Pagar'
        }
    }
}
