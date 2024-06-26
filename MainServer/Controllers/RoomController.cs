using MainServer.Models;
using MainServer.Models.Dtos;
using MainServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IPlayerService _playerService;

        public RoomController(IRoomService roomService, IPlayerService playerService)
        {
            _roomService = roomService;
            _playerService = playerService;
        }

        [HttpPost("CreateRoom")]
        public IActionResult CreateRoom([FromBody] CreateRoomRequest request)
        {
            try
            {
                var roomId = _roomService.CreateRoom(request.Name, request.PlayerToken);
                var player = _playerService.GetPlayerByToken(request.PlayerToken);
                player.IsReady = true;
                _roomService.AddPlayerToRoom(roomId, player!);
                return Ok(roomId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{roomId}/AddPlayerToRoom")]
        public IActionResult AddPlayerToRoom(string roomId, [FromBody] AddPlayerToRoomRequest request)
        {
            try
            {
                var player = _playerService.GetPlayerByToken(request.PlayerToken);
                _roomService.AddPlayerToRoom(roomId, player!);
                return Ok(roomId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{roomId}/RemovePlayerFromRoom/{playerId}")]
        public IActionResult RemovePlayerFromRoom(string roomId, string playerToken)
        {
            _playerService.RemovePlayerAtRoom(playerToken);
            _roomService.RemovePlayerFromRoom(roomId, playerToken);
            return Ok();
        }

        [HttpGet("{roomId}")]
        public IActionResult GetRoom(string roomId)
        {
            try
            {
                var room = _roomService.GetRoom(roomId);
                if (room == null)
                {
                    return NotFound();
                }

                var roomDetailDto = new RoomDetailDto
                {
                    Id = room.Id,
                    Name = room.Name,
                    Players = room.Players.Select(p => new WaitingPlayerDto
                    {
                        TokenPlayer = p.Token,
                        IsReady = p.IsReady
                    }).ToList(),
                    TokenPlayerAsHost = room.TokenPlayerAsHost,
                    StartTime = room.PlayStartTime
                };

                return Ok(roomDetailDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllRooms")]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomService.GetAllRooms().Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                WaitingPlayersCount = room.Players.Count
            }).ToList();

            return Ok(rooms);
        }

        [HttpPost("{roomId}/SetPlayerReady")]
        public IActionResult SetPlayerReady(string roomId, [FromBody] SetPlayerReadyRequest request)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null)
            {
                return NotFound();
            }

            var player = room.Players.Find(p => p.Token == request.PlayerToken);
            if (player == null)
            {
                return NotFound();
            }

            _roomService.SetPlayerReadyStatus(request.PlayerToken, roomId, request.IsReady);
            return Ok();
        }

        [HttpPost("{roomId}/StartGame")]
        public IActionResult StartGame(string roomId, [FromBody] StartGameRequest request)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null || room.TokenPlayerAsHost != request.HostToken)
            {
                return Unauthorized();
            }

            _roomService.StartGame(roomId, request.HostToken);
            return Ok(new { StartTime = DateTime.UtcNow.AddSeconds(3) });
        }

    }
}
