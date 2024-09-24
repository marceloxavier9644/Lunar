using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LunarBase.Utilidades
{
    public class Generica
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

        public string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return String.Empty;

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static string RemoveCaracteres(string texto)
        {
            string ret;
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\\s]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            ret = rgx.Replace(texto, replacement);
            return ret;
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
