namespace ArbedeAPI.Services
{
    public interface IUserService
    {
        Task<(bool Success, string Message, object? Data)> RegisterAsync(string username);
        Task<(bool Success, string Message, object? Data)> LoginAsync(string username);
    }
}
