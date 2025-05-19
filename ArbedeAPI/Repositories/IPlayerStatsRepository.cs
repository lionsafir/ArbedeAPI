using ArbedeAPI.Models;
using System.Threading.Tasks;


public interface IPlayerStatsRepository
{
    Task<PlayerStatsModel?> GetStatsByUserIdAsync(string userId);
}
