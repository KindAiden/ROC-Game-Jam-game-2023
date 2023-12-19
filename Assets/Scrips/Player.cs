using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D hitbox;
    public float maxSpeed;
    public float speed;
    public float jumpHeight;
    public float maxJumps;
    public float jumps;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (GameManager.paused)
            return;

        Vector2 velocity = body.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (grounded())
            jumps = maxJumps - 1;

        if (jumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpHeight;
            jumps--;
        }

        body.velocity = velocity;
    }


    private bool grounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(hitbox.bounds.center, hitbox.bounds.size, 0, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit2D.collider != null;
    }
}
