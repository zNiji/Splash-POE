using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] public int points = 0;
    [SerializeField] public TMP_Text pointsText;
    public TMP_Text pointsTextDeath;

    private int levelsCompleted = 0;

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
        pointsText.text = "Score: " + this.points.ToString();
    }

    public void UpdatePointsUIDeath()
    {
        pointsTextDeath.text = "Final Score: " + this.points.ToString();
    }
}
