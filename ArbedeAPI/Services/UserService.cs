using ArbedeAPI.DTOs;
using ArbedeAPI.Models;
using ArbedeAPI.Repositories;
using ArbedeAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<(bool Success, string Message, object? Data)> RegisterAsync(string username)
    {
        var existing = await _repo.GetByUsernameAsync(username);
        if (existing != null)
            return (false, "Bu kullanıcı adı zaten alınmış.", null);

        var userId = Guid.NewGuid().ToString();
        var newUser = new UserModel { Uid = userId, Username = username };

        await _repo.CreateUserAsync(newUser);
        await _repo.CreatePlayerStatsAsync(userId, new PlayerStatsModel());

        return (true, "Kayıt başarılı", new { Uid = userId, Username = username });
    }

    public async Task<(bool Success, string Message, object? Data)> LoginAsync(string username)
    {
        var user = await _repo.GetByUsernameAsync(username);
        if (user == null)
            return (false, "Kullanıcı bulunamadı", null);

        var stats = await _repo.GetPlayerStatsAsync(user.Uid);
        if (stats == null)
            return (false, "Stats bulunamadı", null);

        return (true, "Giriş başarılı", new
        {
            user.Uid,
            user.Username,
            stats.Gold,
            stats.Trophies,
            stats.Herostone,
            stats.MaviKristal,
            stats.Level,
            stats.Experience
        });
    }
}
