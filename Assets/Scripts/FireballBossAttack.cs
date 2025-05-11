using UnityEngine;

public class FireballBossAttack : MonoBehaviour
{
    [SerializeField] float speed = 0;

    private void Update()
    {

        Vector3 forward = transform.forward;

        Vector3 direction = forward * speed;

        transform.position += direction * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ControlSplash>() != null)
        {
            ControlSplash playerController = collision.gameObject.GetComponent<ControlSplash>();
        }

        GameManager.Instance.gameOver();
    }
}
