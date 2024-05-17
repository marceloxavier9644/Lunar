using System;
using System.Security.Cryptography;
using System.Text;

namespace Lunar.Utils.Unimake.GeradoresXML.GeradorChave
{
    public class GeradorChave
    {
        //public string GerarChaveAcesso(int cUF, DateTime dataEmissao, string cnpjEmitente, string mod, string serie, int nNF)
        //{
        //    int cNF = GerarNumeroUnico(nNF, serie); // Gerar código numérico único

        //    StringBuilder chave = new StringBuilder();
        //    chave.Append(cUF.ToString().PadLeft(2, '0')); // cUF
        //    chave.Append(dataEmissao.ToString("yyMM")); // AAMM
        //    chave.Append(cnpjEmitente.PadLeft(14, '0')); // CNPJ
        //    chave.Append(mod.PadLeft(2, '0')); // mod
        //    chave.Append(serie.PadLeft(3, '0')); // serie
        //    chave.Append(nNF.ToString().PadLeft(9, '0')); // nNF
        //    chave.Append("1"); // tpEmis (para ambiente normal de emissão)
        //    chave.Append(cNF.ToString().PadLeft(8, '0')); // cNF
        //    chave.Append(CalcularDigitoVerificador(chave.ToString())); // cDV

        //    return chave.ToString();
        //}
        public string GerarChaveAcesso(int cUF, DateTime dataEmissao, string cnpjEmitente, string mod, string serie, int nNF)
        {
            string anoMes = dataEmissao.ToString("yyMM");
            int cNF = GerarNumeroUnico(nNF, serie);
            string chave = $"{cUF:D2}{anoMes}{cnpjEmitente}{mod}{serie}{nNF:D9}1{cNF:D8}";
            int cDV = CalcularDigitoVerificador(chave);
            string chaveCompleta = $"{chave}{cDV}";

            return chaveCompleta;
        }

        private int GerarNumeroUnico(int nNF, string serie)
        {
            // Combinação do número da nota fiscal (nNF) e da série
            string combinacao = $"{nNF}-{serie}";

            // Calcula um hash único usando o SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinacao));
                int cNF = BitConverter.ToInt32(bytes, 0);
                return Math.Abs(cNF); // Retorna um número positivo
            }
        }

        private int CalcularDigitoVerificador(string chaveParcial)
        {
            int[] pesos = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int soma = 0;

            for (int i = chaveParcial.Length - 1, j = 0; i >= 0; i--, j++)
            {
                int valor = Convert.ToInt32(chaveParcial[i].ToString()) * pesos[j % pesos.Length];
                soma += valor;
            }

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
