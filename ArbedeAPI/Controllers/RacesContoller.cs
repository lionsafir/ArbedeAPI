using Microsoft.AspNetCore.Mvc;
using ArbedeAPI.DTOs;
using ArbedeAPI.Services;

namespace ArbedeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpPost("select")]
        public async Task<IActionResult> SelectRace([FromBody] SelectRaceRequestDto request)
        {
            var result = await _raceService.SelectRaceAsync(request.UserId, request.RaceName);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("check/{userId}")]
        public async Task<IActionResult> CheckUserRace(string userId)
        {
            var response = await _raceService.CheckUserRaceAsync(userId);
            return Ok(response);
        }
    }
}
