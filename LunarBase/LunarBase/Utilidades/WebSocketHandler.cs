using LunarBase.Classes;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace LunarBase.Utilidades
{
    public static class WebSocketHandler
    {
        private static List<WebSocket> _webSockets = new List<WebSocket>();

        public static void AddWebSocket(WebSocket webSocket)
        {
            _webSockets.Add(webSocket);
        }

        public static async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            AddWebSocket(webSocket);

            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = null;

            try
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (result != null && !result.CloseStatus.HasValue)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    // Processa a mensagem recebida e retransmite para todos os clientes conectados
                    await SendMessageToAllAsync(message);

                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no WebSocket: {ex.Message}");
            }
            finally
            {
                if (webSocket.State != WebSocketState.Closed)
                {
                    await webSocket.CloseAsync(result?.CloseStatus ?? WebSocketCloseStatus.NormalClosure, result?.CloseStatusDescription, CancellationToken.None);
                }
                _webSockets.Remove(webSocket);
            }
        }

        public static async Task SendMessageToAllAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var webSocket in _webSockets.ToList())
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    try
                    {
                        await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
                        _webSockets.Remove(webSocket);
                    }
                }
                else
                {
                    _webSockets.Remove(webSocket);
                }
            }
        }

        public static async Task BroadcastMesaUpdate(AtendimentoMesa mesa)
        {
            var message = new
            {
                type = "update",
                entity = "mesa",
                data = mesa
            };

            var json = JsonConvert.SerializeObject(message);
            await SendMessageToAllAsync(json);
        }
    }
}

