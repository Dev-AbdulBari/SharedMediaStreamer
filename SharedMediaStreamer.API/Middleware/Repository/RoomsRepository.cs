using SharedMediaStreamer.API.Middleware.Interfaces;
using SharedMediaStreamer.API.Middleware.Models;

namespace SharedMediaStreamer.API.Middleware.Repository
{
    public class RoomsRepository : IRoomsRepository
    {
        private List<Room> _rooms;

        public RoomsRepository()
        {
            _rooms = new List<Room>();
        }

        public void AddUserToRoom(User user, string roomId)
        {
            var room = GetRoom(roomId);
            room.Users.Add(user);
        }

        public string CreateRoom()
        {
            string roomId = Guid.NewGuid().ToString().Substring(0,4);
            _rooms.Add(new Room(roomId));
            return roomId;
        }
        public Room GetRoom(string roomId)
        {
            foreach(var room in _rooms)
            {
                if (room.RoomId == roomId)
                {
                    return room;
                }
            }

            return null;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _rooms;
        }

        public void RemoveRoom(string roomId)
        {
            _rooms.Remove(GetRoom(roomId));
        }

        public void RemoveUserFromRoom(User user, string roomId)
        {
            var targetRoom = GetRoom(roomId);
            
            foreach (var room in _rooms)
            {
                if (room.RoomId == targetRoom.RoomId)
                {
                    room.Users.Remove(user);
                }
            }
        }
    }
}
