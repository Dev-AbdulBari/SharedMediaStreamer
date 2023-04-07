using System.Net.WebSockets;

namespace SharedMediaStreamer.API.Middleware.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string UserConnectionSocketId { get; set; }
        public WebSocket UserConnectionSocket { get; set; }
    }
}
