using UnityEngine;

public class ControlSplash : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f; // adjust this value to control the movement speed of splash
    [SerializeField] private Transform cameraTransform; //  camera's transform to follow the player
    [SerializeField] private float smoothSpeed = 1.0f; // value to control the camera's movement speed
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -7); // value to control the camera's position relative to the character

    public float obstacleDestroyDistance = 10f;

    public static ControlSplash instance;

    public float Speed { get => speed; set => speed = value; }

    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        // Read the AD keys to move left and right
        float horizontalInput = 0;
        float verticalInput = 1; // always move forward

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

        // Update camera position
        Vector3 desiredCameraPosition = transform.position + offset;
        Vector3 smoothedCameraPosition = Vector3.Lerp(cameraTransform.position, desiredCameraPosition, smoothSpeed);
        cameraTransform.position = smoothedCameraPosition;

        // Make camera face the character
        cameraTransform.LookAt(transform);

        // Calculate the position and size of the box cast
        Vector3 boxCastPosition = transform.position + transform.forward * 10f;
        Vector3 boxCastSize = new Vector3(40f, 40f, 40f);
        Vector3 boxCastDirection = -transform.forward;

        // Cast a box into the scene
        RaycastHit hit;
        if (Physics.BoxCast(boxCastPosition, boxCastSize, boxCastDirection, out hit, Quaternion.identity, 10f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ObjectLayer"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}