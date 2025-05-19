namespace ArbedeAPI.Repositories
{
    public interface IUnitRepository
    {
        Task AddUnitToUserAsync(string userId, string unitName, string raceName,
                            Dictionary<string, object> stats,
                            Dictionary<string, object> points);

    }
}
