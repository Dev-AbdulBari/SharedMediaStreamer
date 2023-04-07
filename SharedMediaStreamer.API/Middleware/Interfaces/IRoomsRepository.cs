using SharedMediaStreamer.API.Middleware.Models;

namespace SharedMediaStreamer.API.Middleware.Interfaces
{
    public interface IRoomsRepository
    {
        public string CreateRoom();
        public Room GetRoom(string roomId);
        public IEnumerable<Room> GetAllRooms();
        public void RemoveRoom(string roomId);
        public void AddUserToRoom(User user, string roomId);
        public void RemoveUserFromRoom(User user, string roomId);
    }
}
