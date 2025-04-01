using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    public int points = 0;
    public TMP_Text pointsText; 

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
            Update(); 
        }
    }

    private void Update()
    {
        UpdatePointsUI();
    }

    void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    void OnSceneLoaded()
    {
        points = 0;
    }
}
