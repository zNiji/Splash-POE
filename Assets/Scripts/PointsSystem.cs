using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    public int points;
    public TMP_Text pointsText;
    public TMP_Text pointsTextDeath;


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
            //Update(); 
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
}
