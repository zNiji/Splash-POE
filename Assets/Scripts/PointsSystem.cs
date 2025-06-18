using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] public int points = 0;
    [SerializeField] public TMP_Text pointsText;
    [SerializeField] public TMP_Text pointsTextDeath;


    void Start()
    {
        points = 0;
        UpdatePointsUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            points += 1;
            UpdatePointsUI();
        }
    }

    public void UpdatePointsUI()
    {
        pointsText.text = "Score: " + this.points.ToString();
    }
}
