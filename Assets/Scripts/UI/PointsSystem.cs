using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] public int points = 0;
    [SerializeField] public TMP_Text pointsText;
    public TMP_Text pointsTextDeath;

    public int levelsCompleted = 0;

    void Awake()
    {
        // Ensure this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        points = 0;
        UpdatePointsUI();

        GameManager.Instance.ObstaclePassed.AddListener(OnObstaclePassed);
        GameManager.Instance.LevelComplete.AddListener(LevelUp);
    }

    private void LevelUp()
    {
        levelsCompleted++;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ObstaclePassed.Invoke();
    }

    public void OnObstaclePassed()
    {
        points += 1;
        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        if (pointsText != null)
            pointsText.text = "Score: " + points.ToString();
    }

    public void UpdatePointsUIDeath()
    {
        if (pointsTextDeath != null)
            pointsTextDeath.text = "Final Score: " + points.ToString();
    }

    // New method to reset points
    public void ResetPoints()
    {
        points = 0;
        levelsCompleted = 0; // Optional: Reset levels completed if needed
        UpdatePointsUI(); // Update the in-game UI to reflect the reset
    }
}
