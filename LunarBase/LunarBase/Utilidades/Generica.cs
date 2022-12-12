using System.Text.RegularExpressions;

namespace LunarBase.Utilidades
{
    public class Generica
    {
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
