using Google.Cloud.Firestore;

using System.Threading.Tasks;
using ArbedeAPI.Models;

public class PlayerStatsRepository : IPlayerStatsRepository
{
    private readonly FirestoreDb _firestore;

    public PlayerStatsRepository(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    public async Task<PlayerStatsModel?> GetStatsByUserIdAsync(string userId)
    {
        var docRef = _firestore.Collection("users").Document(userId)
                               .Collection("stats").Document("playerStats");

        var snapshot = await docRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ConvertTo<PlayerStatsModel>() : null;
    }
}
