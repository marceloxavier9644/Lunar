using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace LunarAtualiza.Utils
{
    public class GenericaDesktop
    {

        public static void ShowInfo(String mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowAlerta(String mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErro(String mensagem)
        {
            MessageBox.Show(mensagem, "Aconteceu algo errado...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowConfirmacao(String mensagem)
        {
            return MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public void ShowAutoClosingMessageBox(string message, string caption)
        {
            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, caption);
                if (hWnd.ToInt32() != 0) PostMessage(hWnd, WM_CLOSE, 0, 0);
            };
            timer.Enabled = true;
            MessageBox.Show(message, caption);
        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

     

        public void buscarAlertaCadastrado(Pessoa pessoa)
        {
            AlertaClienteController alertaClienteController = new AlertaClienteController();
            IList<AlertaCliente> listaAlerta = alertaClienteController.selecionarAlertaPorPessoa(pessoa.Id);
            if (listaAlerta.Count > 0)
            {
                String mensagem = "";
                foreach (AlertaCliente alerta in listaAlerta)
                {
                    if (!String.IsNullOrEmpty(mensagem))
                        mensagem = mensagem + "\n" + alerta.Data.ToShortDateString() + " - " + alerta.Descricao;
                    else
                        mensagem = alerta.Data.ToShortDateString() + " - " + alerta.Descricao;
                }
                GenericaDesktop.ShowAlerta(mensagem);
            }
        }

        public static bool validarCPFCNPJ(String cpfcnpj, Boolean aceitaVazio = false)
        {
            if (string.IsNullOrWhiteSpace(cpfcnpj))
                return aceitaVazio;
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;

                SoNumero = Regex.Replace(cpfcnpj, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }

                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                else return false;
            }
        }
        public static async Task<bool> VerificaProgramaContigenciaEstaEmExecucao()
        {
            Process[] localByName = Process.GetProcessesByName("javaw");
            if (localByName.Length > 0)
            {
                return true;
            }
            else
            {
                if (File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\NSNFCeClient.jar"))
                {
                    Process.Start(Sessao.parametroSistema.PastaRemessaNsCloud + @"\NSNFCeClient.jar");
                    await Task.Delay(2000);
                    return true;
                }
                else
                    return false;
            }
        }
        public bool ValidarEmail(String email)
        {
            bool emailValido = false;

            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;
        }

       
        public static Boolean possuiConexaoInternet()
        {
            Uri url = new Uri("http://www.google.com.br");
            WebRequest request = WebRequest.Create(url);
            WebResponse response;
            try
            {
                request.Proxy = null;
                request.Timeout = 2000;
                response = request.GetResponse();
                response.Close();
                request = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Criptografa(String valor)
        {
            Byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
            String dadosCriptografados = Convert.ToBase64String(bytes);
            return dadosCriptografados;
        }

        public static string Descriptografa(String valor)
        {
            Byte[] bytes = Convert.FromBase64String(valor);
            String dadosDescriptografados = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
            return dadosDescriptografados;
        }
    }
}
