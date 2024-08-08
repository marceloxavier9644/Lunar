using LunarBase.Classes;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;

namespace LunarApi.Controllers
{
    public class AtendimentoMesaWebSocketService
    {
        private readonly WebSocket _webSocket;

        public AtendimentoMesaWebSocketService(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public async Task SendMesasUpdate(List<AtendimentoMesa> mesas)
        {
            var json = JsonSerializer.Serialize(mesas);
            var buffer = Encoding.UTF8.GetBytes(json);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
