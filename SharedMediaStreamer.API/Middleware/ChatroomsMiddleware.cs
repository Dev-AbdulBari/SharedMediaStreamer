using Newtonsoft.Json;
using SharedMediaStreamer.API.Middleware.Interfaces;
using SharedMediaStreamer.API.Middleware.Models;
using SharedMediaStreamer.Domain.Models;
using System.Net.WebSockets;
using System.Text;

namespace SharedMediaStreamer.API.Middleware
{
    public class ChatroomsMiddleware
    {
        private static IRoomsRepository _roomsRepository;

        private readonly RequestDelegate _next;

        public ChatroomsMiddleware(RequestDelegate next, IRoomsRepository roomsRepository)
        {
            _next = next;
            _roomsRepository = roomsRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            CancellationToken ct = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            var RequestQueryStringParams = context.Request.Query;
            var userName = RequestQueryStringParams["username"];
            var roomId = RequestQueryStringParams["room"];
            var socketId = Guid.NewGuid().ToString();
            var currentUser = new User()
            {
                UserName = userName,
                UserConnectionSocketId = socketId,
                UserConnectionSocket = currentSocket
            };

            _roomsRepository.AddUserToRoom(currentUser, roomId);

            foreach (var user in _roomsRepository.GetRoom(roomId).Users)
            {
                if (user.UserConnectionSocket.State != WebSocketState.Open)
                {
                    continue;
                }

                await SendStringAsync(user.UserConnectionSocket, "{\"messageEventType\":1,\"message\":\"" + currentUser.UserName + " has joined \"}", ct);
            }

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                var response = await ReceiveStringAsync(currentSocket, ct);
                var chatMessageEvent = JsonConvert.DeserializeObject<ChatMessageEvent>(response);

                if (string.IsNullOrEmpty(response))
                {
                    if (currentSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    continue;
                }

                foreach (var user in _roomsRepository.GetRoom(roomId).Users)
                {
                    if (user.UserConnectionSocket.State != WebSocketState.Open)
                    {
                        continue;
                    }


                    if (user.UserConnectionSocket == currentSocket && chatMessageEvent.MessageEventType == MessageEventType.VideoTimePlay |
                        chatMessageEvent.MessageEventType == MessageEventType.VideoTimePause)
                    {
                        continue;
                    }

                    await SendStringAsync(user.UserConnectionSocket, response, ct);
                }
            }

            _roomsRepository.RemoveUserFromRoom(currentUser, roomId);

            foreach (var user in _roomsRepository.GetRoom(roomId).Users)
            {
                await SendStringAsync(user.UserConnectionSocket, "{\"messageEventType\":1,\"message\":\"" + currentUser.UserName + " has left \"}", ct);
            }

            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            currentSocket.Dispose();
        }

        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
