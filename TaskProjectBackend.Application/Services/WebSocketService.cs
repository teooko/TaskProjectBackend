using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace TaskProjectBackend.Application.Services;
using Fleck;

public class WebSocketService
{
    private readonly List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
    public ConcurrentDictionary<int, ConcurrentBag<IWebSocketConnection>> Rooms { get; } = new ConcurrentDictionary<int, ConcurrentBag<IWebSocketConnection>>();
    private WebSocketServer _server;

    static int ExtractRoomIdFromUrl(string path)
    {
        if(path == "/")
            return -1;
        if (!string.IsNullOrEmpty(path) && path.StartsWith("/"))
        {
            return Int32.Parse(path.TrimStart('/'));
        }

        return -1;
    }
    
    public void BroadcastToRoom(int roomId, string message)
    {
        var connections = Rooms[roomId];
        foreach (var connection in connections)
        {
            Console.WriteLine(connections.Count + " ATATEA IS");
            connection.Send(message);
        }
    }
    
    public void AddConnectionToRoom(int roomId, IWebSocketConnection connection)
    {
        var roomConnections = Rooms.GetOrAdd(roomId, _ => new ConcurrentBag<IWebSocketConnection>());
        roomConnections.Add(connection);
    }
    
    public void StartWebSocketServer(string url)
    {
        _server = new WebSocketServer(url);
        _server.Start(socket =>
        {
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
                int roomId = ExtractRoomIdFromUrl(socket.ConnectionInfo.Path);
                Console.WriteLine(roomId + " ROOOOOOOOOOOOOOOOOOOOOOOOOOOOM IDDDDDDDDDDDDDDDDDDDDD");
                if(roomId > -1)
                {
                    if(message == "connected") 
                        AddConnectionToRoom(roomId, socket);
                    else if(Rooms[roomId].Contains(socket))
                        BroadcastToRoom(roomId, message);
                }
                else
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
