using ArbedeAPI.Models;
using Google.Cloud.Firestore;

namespace ArbedeAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByUsernameAsync(string username);
        Task<UserModel?> GetByIdAsync(string userId);
        Task CreateUserAsync(UserModel user);
        Task CreatePlayerStatsAsync(string userId, PlayerStatsModel stats);
        Task<PlayerStatsModel?> GetPlayerStatsAsync(string userId);

        Task<bool> UserExistsAsync(string userId);
        Task<bool> UserHasRaceAsync(string userId);
        Task SetUserRaceAsync(string userId, string raceName, Timestamp selectedAt);

        Task<Dictionary<string, object>?> GetUserRawDataAsync(string userId);

    }
}
