namespace ArbedeAPI.Models;
using Google.Cloud.Firestore;

[FirestoreData]        // Bu sınıf Firestore'a kaydedilebilir
public class UserModel
{
    [FirestoreProperty]
    public string Uid { get; set; }

    [FirestoreProperty]
    public string Username { get; set; }

    // Optional: race seçildiyse
    public string? Race { get; set; }



    // Boş (parametresiz) bir constructor da bulunsun
    public UserModel() { }
}
