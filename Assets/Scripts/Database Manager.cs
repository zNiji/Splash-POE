using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour
{
    private DatabaseReference databaseReference;

    public static Database instance; // Singleton instance of Database

    [SerializeField] private PointsSystem pointsSystem;

    void Awake()
    {
        // Ensure this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);

        // Check if the instance is already created
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Get a reference to the database
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        pointsSystem = FindFirstObjectByType<PointsSystem>();
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