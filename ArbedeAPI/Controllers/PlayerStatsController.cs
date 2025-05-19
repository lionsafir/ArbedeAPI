using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArbedeAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class PlayerStatsController : ControllerBase
{
    private readonly IPlayerStatsService _playerStatsService;

    public PlayerStatsController(IPlayerStatsService service)
    {
        _playerStatsService = service;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetPlayerStats(string userId)
    {
        var stats = await _playerStatsService.GetStatsAsync(userId);
        return stats == null ? NotFound() : Ok(stats);
    }
}
