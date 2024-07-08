using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunarSoftwareAtivador
{
    public partial class FrmTermosDeUso : Form
    {
        ToolTip toolTip = new ToolTip();
        public FrmTermosDeUso()
        {
            InitializeComponent();
            carregarTexto();
            mostrarInformacao();
        }
        private void carregarTexto()
        {
            rtbTermos.Text = "Termos de Uso do Sistema Lunar\r\nÚltima atualização: [30/06/2024]\r\n\n" +
                "1. Aceitação dos Termos\r\n\r\nAo utilizar o sistema Lunar, você concorda em cumprir e estar sujeito a estes " +
                "Termos de Uso. Caso não concorde com algum dos termos aqui apresentados, você não deve utilizar " +
                "o sistema.\r\n\r\n2. Criador e Responsável pelo Sistema\r\n\r\nO sistema Lunar foi desenvolvido e " +
                "é mantido por Lunar Software LTDA. Toda comunicação oficial e suporte devem ser realizados " +
                "através dos canais disponibilizados pela Lunar Software.\r\n\r\n3. Licenciamento e Uso do Sistema\r\n\r\nO uso " +
                "do sistema Lunar é permitido apenas para clientes que contrataram o sistema e obtiveram uma licença válida junto " +
                "à Lunar Software.\r\nA licença de uso é concedida de forma não exclusiva e intransferível, sendo vedado o uso " +
                "do sistema sem a devida contratação e validação da licença.\r\n\n4. Responsabilidade pelo Backup\r\n\r\nA " +
                "responsabilidade pela realização de backups dos dados gerados e manipulados no sistema Lunar é exclusivamente do " +
                "cliente.\r\nA Lunar Software não se responsabiliza por perda de dados, sendo recomendado que o cliente implemente " +
                "rotinas regulares de backup.\r\n\n5. Hardware e Infraestrutura\r\n\r\nA Lunar Software não se responsabiliza por " +
                "defeitos ou danos no hardware do cliente.\r\nO sistema Lunar é instalado localmente no computador do cliente, " +
                "e a manutenção de hardware é de inteira responsabilidade do cliente.\r\n\n6. Treinamento e Implantação\r\n\r\nPara " +
                "garantir a melhor usabilidade do sistema, é recomendado contratar o plano de treinamento e implantação oferecido " +
                "pela Lunar Software.\r\nA Lunar Software não se responsabiliza pelo treinamento de novos funcionários em caso de " +
                "troca de equipe na empresa cliente. A empresa cliente deve garantir que haja um responsável interno para treinar " +
                "novos funcionários ou contratar um novo plano de treinamento junto à Lunar Software ou seus parceiros.\r\n\n" +
                "7. Limitação de Responsabilidade\r\n\r\nA Lunar Software não se responsabiliza por furos de estoque, danos " +
                "fiscais ou qualquer outra consequência decorrente do uso inadequado do sistema.\r\nO sistema Lunar é uma " +
                "ferramenta eficaz para controle de diversas situações, desde que operado por uma equipe devidamente capacitada.\r\n\n" +
                "8. Uso Indevido\r\n\r\nÉ expressamente proibido usar o sistema Lunar para atividades ilegais ou não autorizadas.\r\n" +
                "Qualquer uso do sistema que viole estes Termos de Uso poderá resultar na suspensão ou cancelamento da " +
                "licença de uso, sem prejuízo das medidas legais cabíveis.\r\n\n9. Atualizações e Manutenções\r\n\r\nA Lunar " +
                "Software se reserva o direito de realizar atualizações e manutenções no sistema a qualquer momento. " +
                "O cliente será notificado previamente sempre que possível.\r\nAs atualizações podem incluir, mas " +
                "não se limitam a, melhorias, correções de bugs e novas funcionalidades.\r\n" +
                "10. Suporte Técnico\r\n\r\nO suporte técnico é fornecido conforme o plano de suporte contratado pelo cliente.\r\n\n" +
                "O suporte não cobre problemas relacionados ao hardware do cliente ou à infraestrutura de rede local.\r\n\n" +
                "11. Responsabilidades do Cliente\r\n\r\nO cliente é responsável por garantir que os dados inseridos " +
                "no sistema estão corretos e atualizados.\r\nO cliente deve garantir que todos os usuários do sistema na sua " +
                "empresa estão cientes e de acordo com estes Termos de Uso.\r\n\n12. Proteção de Dados\r\n\r\nA Lunar Software " +
                "implementa medidas para proteger os dados armazenados no sistema, porém, a segurança completa " +
                "depende também das práticas adotadas pelo cliente.\r\nRecomenda-se que o cliente adote práticas " +
                "de segurança como o uso de senhas fortes, controle de acesso e a realização de backups regulares.\r\n\n" +
                "13. Garantias Limitadas\r\n\r\nO sistema Lunar é fornecido \"no estado em que se " +
                "encontra\" e \"conforme disponível\", sem garantias de qualquer tipo, expressas ou implícitas, " +
                "incluindo, mas não se limitando a, garantias de comercialização, adequação a um propósito " +
                "específico e não violação.\r\nA Lunar Software não garante que o sistema será ininterrupto ou " +
                "livre de erros.\r\n\n14. Alterações nos Termos de Uso\r\n\r\nA Lunar Software se reserva o direito de alterar estes " +
                "Termos de Uso a qualquer momento, mediante notificação ao cliente.\r\nO uso contínuo do sistema após a publicação " +
                "das alterações constituirá aceitação dos novos termos.\r\n\n15. Jurisdição e Lei Aplicável\r\n\r\nEstes Termos de " +
                "Uso são regidos pelas leis do Brasil.\r\nQualquer disputa relacionada a estes Termos de Uso será resolvida nos " +
                "tribunais competentes em Unaí MG, salvo disposição em contrário pela legislação " +
                "aplicável.\r\n\n16. Contato\r\n\r\nPara qualquer dúvida ou solicitação relacionada a estes Termos de Uso, entre " +
                "em contato com o suporte da Lunar Software através dos canais oficiais.\r\n\n" +
                "17. Disposições Gerais\r\n\r\nA nulidade ou inaplicabilidade de qualquer disposição destes Termos de " +
                "Uso não afetará a validade ou aplicabilidade das demais disposições.\r\nEstes Termos de Uso constituem o " +
                "acordo integral entre o cliente e a Lunar Software em relação ao uso do sistema, substituindo quaisquer " +
                "acordos anteriores.\r\nAo clicar em \"Aceito os termos\", você concorda com os Termos de Uso acima descritos.";
        }

        private async void mostrarInformacao()
        {
            await Task.Delay(1000);
            toolTip.ToolTipTitle = "Informação";
            toolTip.UseFading = true;
            toolTip.UseAnimation = true;
            toolTip.IsBalloon = true;
            toolTip.ShowAlways = true;
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.Show("Clique aqui para aceitar os termos", cbAceite, 0, -65, 5000);
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbAceite_CheckedChanged(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
