namespace ArbedeAPI.Repositories
{
    public interface IRaceRepository
    {
        Task<Dictionary<string, object>?> GetUnitStatsAsync(string raceName, string unitType);
        Task<Dictionary<string, object>?> GetUnitPointsAsync(string raceName, string unitType);
    }
}
