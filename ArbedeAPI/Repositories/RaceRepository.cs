using ArbedeAPI.Repositories;
using Google.Cloud.Firestore;

public class RaceRepository : IRaceRepository
{
    private readonly FirestoreDb _firestore;

    public RaceRepository(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    public async Task<Dictionary<string, object>?> GetUnitStatsAsync(string raceName, string unitType)
    {
        var statsRef = _firestore
            .Collection("Races")
            .Document(raceName)
            .Collection("Units")
            .Document(unitType)
            .Collection("1")
            .Document("Stats");

        var snapshot = await statsRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ToDictionary() : null;
    }

    public async Task<Dictionary<string, object>?> GetUnitPointsAsync(string raceName, string unitType)
    {
        var pointsRef = _firestore
            .Collection("Races")
            .Document(raceName)
            .Collection("Units")
            .Document(unitType)
            .Collection("1")
            .Document("Points");

        var snapshot = await pointsRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ToDictionary() : null;
    }
}
