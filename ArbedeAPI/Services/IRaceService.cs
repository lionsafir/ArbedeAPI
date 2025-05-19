using ArbedeAPI.DTOs;

namespace ArbedeAPI.Services
{
    public interface IRaceService
    {
        Task<(bool Success, string Message, object? Data)> SelectRaceAsync(string userId, string raceName);
        Task<RaceCheckResponseDto> CheckUserRaceAsync(string userId);

    }
}
