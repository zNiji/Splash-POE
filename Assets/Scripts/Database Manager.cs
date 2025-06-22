using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Database : MonoBehaviour
{
    private DatabaseReference databaseReference;

    [SerializeField] private PointsSystem pointsSystem;

    void Start()
    {
        // Get a reference to the database
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void StorePlayerProgress(int levelsCompleted, int points)
    {
        PlayerProgress playerProgress = new PlayerProgress(levelsCompleted, points);
        string json = JsonUtility.ToJson(playerProgress);
        databaseReference.Child("player_progress").UpdateChildrenAsync(new Dictionary<string, object>
    {
        { "levelsCompleted", playerProgress.levelsCompleted },
        { "points", playerProgress.points }
    });
    }

    public async Task<PlayerProgress> RetrievePlayerProgressAsync()
    {
        DataSnapshot dataSnapshot = await databaseReference.Child("player_progress").GetValueAsync();
        if (dataSnapshot.Exists)
        {
            return JsonUtility.FromJson<PlayerProgress>(dataSnapshot.GetRawJsonValue());
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class PlayerProgress
{
    public int levelsCompleted;
    public int points;

    public PlayerProgress(int levelsCompleted, int points)
    {
        this.levelsCompleted = levelsCompleted;
        this.points = points;
    }
}