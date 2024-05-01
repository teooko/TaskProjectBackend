using System.Net.WebSockets;

namespace TaskProjectBackend.Application.Services;
using Fleck;

public class WebSocketService
{
    private readonly List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
    private readonly int _maxClients = 4;
    private WebSocketServer _server;

    public void StartWebSocketServer(string url)
    {
        _server = new WebSocketServer(url);
        _server.Start(socket =>
        {
            if (_clients.Count >= _maxClients)
            {
                socket.Close();
                return;
            }
            
            socket.OnOpen = () =>
            {
                Console.WriteLine("WebSocket opened");
                _clients.Add(socket);
            };

            socket.OnClose = () =>
            {
                Console.WriteLine("WebSocket closed");
                _clients.Remove(socket);
            };

            socket.OnMessage = message =>
            {
                Console.WriteLine($"Received message: {message}");
                Broadcast(message);
            };
        });
    }

    public void Broadcast(string message)
    {
        foreach (var client in _clients)
        {
            client.Send(message);
        }
    }
    
    public void StopWebSocketServer()
    {
        //Console.WriteLine(_server.Port + "AAAAAAAAAAAAAAAAAAAAAAa");
        _server.Dispose(); // Stop the server and release resources
        _server = null; // Clear the reference to the server instance
        _clients.Clear(); // Clear the list of clients
        Console.WriteLine("WebSocket server stopped");
    }
}
