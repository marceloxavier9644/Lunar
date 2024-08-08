using LunarApi;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços necessários
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5130); // Configura para escutar em todas as interfaces na porta 5130
});

var app = builder.Build();

// Inicializa NHibernate
InitializeNHibernate.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Desativar o redirecionamento HTTPS
// app.UseHttpsRedirection(); // Comente ou remova esta linha

app.UseAuthorization();

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws" && context.Request.Headers["Upgrade"] == "websocket")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await LunarBase.Utilidades.WebSocketHandler.HandleWebSocketConnection(webSocket);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    else
    {
        await next();
    }
});

app.MapControllers();
app.Run();
