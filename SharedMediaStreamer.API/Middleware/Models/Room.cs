namespace SharedMediaStreamer.API.Middleware.Models
{
    public class Room
    {
        public Room(string roomId)
        {
            RoomId = roomId;
            Users = new List<User>();
        }

        public string RoomId { get; set; }
        public List<User> Users { get; set; }
    }
}
