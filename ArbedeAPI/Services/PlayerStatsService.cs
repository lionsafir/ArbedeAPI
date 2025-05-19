using ArbedeAPI.Models;
using ArbedeAPI.Services;
using System.Threading.Tasks;

public class PlayerStatsService : IPlayerStatsService
{
    private readonly IPlayerStatsRepository _repository;

    public PlayerStatsService(IPlayerStatsRepository repository)
    {
        _repository = repository;
    }

    public async Task<PlayerStatsModel?> GetStatsAsync(string userId)
    {
        return await _repository.GetStatsByUserIdAsync(userId);
    }
}
