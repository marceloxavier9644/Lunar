using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lunar.Telas.Food
{
    public class UpdateTela
    {
        private static async Task ConnectWebSocketAsync()
        {
            using (ClientWebSocket client = new ClientWebSocket())
            {
                try
                {
                    // Conectando ao WebSocket
                    Uri serverUri = new Uri("ws://localhost:5000/ws"); // Substitua com a URL do seu WebSocket
                    await client.ConnectAsync(serverUri, CancellationToken.None);
                    Console.WriteLine("Conectado ao WebSocket.");

                    // Iniciar a tarefa para receber mensagens
                    var receiveTask = ReceiveMessagesAsync(client);

                    // Enviar uma mensagem para o WebSocket (opcional)
                    string messageToSend = "Hello, WebSocket!";
                    var sendBuffer = Encoding.UTF8.GetBytes(messageToSend);
                    var sendSegment = new ArraySegment<byte>(sendBuffer);
                    await client.SendAsync(sendSegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine($"Mensagem enviada: {messageToSend}");

                    // Aguarda a tarefa de recebimento de mensagens terminar
                    await receiveTask;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        private static async Task ReceiveMessagesAsync(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];
            while (client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;
                var receiveSegment = new ArraySegment<byte>(buffer);
                try
                {
                    result = await client.ReceiveAsync(receiveSegment, CancellationToken.None);

                    // Converte a mensagem recebida para string e exibe
                    string messageReceived = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Mensagem recebida: {messageReceived}");

                    // Processar a mensagem recebida (opcional)
                    // Exemplo: Atualizar uma interface, log, etc.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao receber mensagem: {ex.Message}");
                    break;
                }
            }
        }


    }
}
