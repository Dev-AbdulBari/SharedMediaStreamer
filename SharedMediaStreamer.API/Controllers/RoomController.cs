using Microsoft.AspNetCore.Mvc;
using SharedMediaStreamer.API.Middleware.Interfaces;

namespace SharedMediaStreamer.API.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private IRoomsRepository _roomsRepository;
        public RoomController(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            return Ok(_roomsRepository.GetAllRooms());
        }

        [HttpGet("{roomId}")]
        public IActionResult GetRoom(string roomId)
        {
            return Ok(_roomsRepository.GetRoom(roomId));
        }

        [HttpPost]
        public IActionResult CreateRoom()
        {
            return Ok(_roomsRepository.CreateRoom());
        }
    }
}
