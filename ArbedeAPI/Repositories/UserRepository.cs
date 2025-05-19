namespace ArbedeAPI.Repositories;
using Google.Cloud.Firestore;
using ArbedeAPI.Models;

public class UserRepository : IUserRepository
{
    private readonly FirestoreDb _firestore;

    public UserRepository()
    {
        _firestore = FirestoreDb.Create("yonet-ve-fethet");
    }

    public async Task<UserModel?> GetByUsernameAsync(string username)
    {
        var snap = await _firestore.Collection("users")
            .WhereEqualTo("Username", username)
            .Limit(1)
            .GetSnapshotAsync();
        return snap.Count == 0 ? null : snap.Documents[0].ConvertTo<UserModel>();
    }

    public async Task<UserModel?> GetByIdAsync(string userId)
    {
        var snap = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();
        return snap.Exists ? snap.ConvertTo<UserModel>() : null;
    }

    public async Task CreateUserAsync(UserModel user)
    {
        var doc = _firestore.Collection("users").Document(user.Uid);
        await doc.SetAsync(user);
    }

    public async Task CreatePlayerStatsAsync(string userId, PlayerStatsModel stats)
    {
        var statsDoc = _firestore.Collection("users").Document(userId)
            .Collection("stats").Document("playerStats");
        await statsDoc.SetAsync(stats);
    }

    public async Task<PlayerStatsModel?> GetPlayerStatsAsync(string userId)
    {
        var doc = await _firestore.Collection("users").Document(userId)
            .Collection("stats").Document("playerStats").GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<PlayerStatsModel>() : null;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        var doc = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();
        return doc.Exists;
    }

    public async Task<bool> UserHasRaceAsync(string userId)
    {
        var doc = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();
        return doc.Exists && doc.ContainsField("race");
    }

    public async Task SetUserRaceAsync(string userId, string raceName, Timestamp selectedAt)
    {
        var docRef = _firestore.Collection("users").Document(userId);
        var updates = new Dictionary<string, object>
        {
            { "race", raceName },
            { "raceSelectedAt", selectedAt }
        };
        await docRef.UpdateAsync(updates);
    }

    public async Task<Dictionary<string, object>?> GetUserRawDataAsync(string userId)
    {
        var snapshot = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ToDictionary() : null;
    }

}

