using MainServer.Models;
using MainServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("Register")]
        public IActionResult Register()
        {
            Player _player = _playerService.RegisterPlayer();
            return Ok(_player.Token);
        }

    }
}
