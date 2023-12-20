using UnityEngine;

public class DeadlyBot : MonoBehaviour
{
    public float speed = 2.0f;
    private GameObject player;
    private BoxCollider2D hitbox;
    private SpriteRenderer sprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitbox = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (direction.x < 0)
            sprite.flipX = true;
        else if (direction.x > 0)
            sprite.flipX = false;
        //update the boxcast
        RaycastHit2D cast = Physics2D.BoxCast(hitbox.bounds.center, new Vector2(hitbox.bounds.size.x, hitbox.bounds.size.y * 0.8f), 0, direction, 0.1f, LayerMask.GetMask("Player"));
        //check if this enemy is touching the player
        if (cast.collider != null)
        {
            GameManager.GameOver();
        }
    }

    public void Die()
    {
        GameObject.Find("Main Camera").GetComponent<CameraBehavior>().objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
