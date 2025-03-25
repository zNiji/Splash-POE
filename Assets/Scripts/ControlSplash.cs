using UnityEngine;

public class ControlSplash : MonoBehaviour
{
    public float speed = 5.0f; // adjust this value to control the movement speed

    // Update is called once per frame
    void Update()
    {
        // Read the WASD keys
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
        }

        // Create a movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Apply movement to the ball
        transform.position += movement * speed * Time.deltaTime;
    }
}