using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            await GameManager.Instance.gameOver();
        }
    }
}
