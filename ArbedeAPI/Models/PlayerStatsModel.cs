using Google.Cloud.Firestore;

namespace ArbedeAPI.Models
{
    [FirestoreData]
    public class PlayerStatsModel
    {
        [FirestoreProperty("Gold")]
        public int Gold { get; set; }

        [FirestoreProperty("Trophies")]
        public int Trophies { get; set; }

        [FirestoreProperty("Herostone")]
        public int Herostone { get; set; }

        [FirestoreProperty("MaviKristal")]
        public int MaviKristal { get; set; }

        [FirestoreProperty("Level")]
        public int Level { get; set; }

        [FirestoreProperty("Experience")]
        public int Experience { get; set; }




    }
}
