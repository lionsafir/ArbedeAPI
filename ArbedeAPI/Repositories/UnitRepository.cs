using ArbedeAPI.Repositories;
using Google.Cloud.Firestore;

public class UnitRepository : IUnitRepository
{
    private readonly FirestoreDb _firestore;

    public UnitRepository(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    public async Task AddUnitToUserAsync(string userId, string unitName, string raceName,
                                         Dictionary<string, object> stats,
                                         Dictionary<string, object> points)
    {
        var userUnitDocRef = _firestore
            .Collection("users").Document(userId)
            .Collection("units").Document(unitName);

        // Ana birim dokümanı (örneğin unitType ve race bilgisi)
        await userUnitDocRef.SetAsync(new Dictionary<string, object>
        {
            { "unitType", unitName },
            { "race", raceName }
        });

        var levelCollectionRef = userUnitDocRef.Collection("1");

        await levelCollectionRef.Document("UnitStats").SetAsync(stats);
        await levelCollectionRef.Document("UnitPoints").SetAsync(points);
    }
}
