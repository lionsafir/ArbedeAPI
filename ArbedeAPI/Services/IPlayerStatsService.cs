using ArbedeAPI.Models;

namespace ArbedeAPI.Services
{
    public interface IPlayerStatsService
    {
        Task<PlayerStatsModel?> GetStatsAsync(string userId);
    }
}
