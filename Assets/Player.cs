using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the player
        startPosition = transform.position;
    }

    void Update()
    {
        // Handle player movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, z);
    }

    public void Respawn()
    {
        // Reset the player's position to the start position
        transform.position = startPosition;
        // You can also reset other states of the player if necessary
    }
}
