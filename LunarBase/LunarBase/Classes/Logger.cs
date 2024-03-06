namespace LunarBase.Classes
{
    public class Logger
    {


        public Logger()
        {

        }

        public void WriteLog(string mensagem, string pasta)
        {
            try
            {
                // Obtém a data atual
                DateTime currentDate = DateTime.Now.Date;
                // Cria o nome do arquivo com base na data atual
                string fileName = $"{currentDate:yyyy-MM-dd}_log.txt";
                // Combina o caminho do diretório raiz com o nome da pasta e o nome do arquivo
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pasta);
                string filePath = Path.Combine(logDirectory, fileName);

                // Verifica se a pasta de log existe, se não existir, cria-a
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Abre o arquivo de log em modo de escrita, adicionando ao final do arquivo
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    // Formata a mensagem com a data e hora atuais
                    string logMessage = $"{DateTime.Now} - {mensagem}";
                    // Escreve a mensagem no arquivo
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro ao escrever no arquivo, imprime o erro no console
                Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
            }
        }
    }
}
