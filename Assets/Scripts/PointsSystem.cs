using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    private int points = 0;
    public Text pointsText; 

    void Start()
    {
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

    void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points.ToString();
    }
}
