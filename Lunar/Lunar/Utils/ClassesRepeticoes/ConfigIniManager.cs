using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.ClassesRepeticoes
{
    public class ConfigIniManager
    {
        private string caminhoArquivo;

        // Construtor para definir o caminho do arquivo ini
        public ConfigIniManager(string nomeArquivo = "Config.ini")
        {
            caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
        }

        // Método para salvar os dados no arquivo .ini
        public void Etiquetas_SalvarConfiguracao(string modeloEtiqueta, string impressora, string parcelas, string observacoes)
        {
            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {
                writer.WriteLine("[Etiquetas]");
                writer.WriteLine("ModeloEtiqueta=" + modeloEtiqueta);
                writer.WriteLine("Impressora=" + impressora);
                writer.WriteLine("Parcelas=" + parcelas);
                writer.WriteLine("Observacoes=" + observacoes);
            }
        }

        // Método para carregar os dados do arquivo .ini
        public (string modeloEtiqueta, string impressora, string parcelas, string observacoes) Etiquetas_CarregarConfiguracao()
        {
            string modeloEtiqueta = string.Empty;
            string impressora = string.Empty;
            string parcelas = string.Empty;
            string observacoes = string.Empty;

            if (File.Exists(caminhoArquivo))
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);
                bool etiquetasSection = false;

                foreach (string linha in linhas)
                {
                    if (linha.Trim() == "[Etiquetas]")
                    {
                        etiquetasSection = true;
                    }
                    else if (linha.StartsWith("[") && linha.EndsWith("]"))
                    {
                        etiquetasSection = false;
                    }

                    if (etiquetasSection)
                    {
                        if (linha.StartsWith("ModeloEtiqueta="))
                        {
                            modeloEtiqueta = linha.Split('=')[1];
                        }

                        if (linha.StartsWith("Impressora="))
                        {
                            impressora = linha.Split('=')[1];
                        }

                        if (linha.StartsWith("Parcelas="))
                        {
                            parcelas = linha.Split('=')[1];
                        }
                        if (linha.StartsWith("Observacoes="))
                        {
                            observacoes = linha.Split('=')[1];
                        }
                    }
                }
            }

            return (modeloEtiqueta, impressora, parcelas, observacoes);
        }
    }
}