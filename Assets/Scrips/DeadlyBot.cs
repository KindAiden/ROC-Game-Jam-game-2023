using UnityEngine;

public class DeadlyBot : MonoBehaviour
{
    public float speed = 2.0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Player eliminated!");
            GameManager.GameOver();
        }
    }
}
