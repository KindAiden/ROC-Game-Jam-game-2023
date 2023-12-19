using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D body;
    private BoxCollider2D hitbox;
    private Animator animator;
    public float speed;
    public float jumpHeight;
    public float maxJumps;
    public float jumps;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.paused)
            return;

        animator.speed = 1;
        if (body.velocity.x < 0)
            sprite.flipX = true;
        else if (body.velocity.x > 0)
            sprite.flipX = false;
        else
        {
            // Pause the animation and reset it to the first frame
            animator.speed = 0f;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
        }

        Vector2 velocity = body.velocity;

        float newspeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            newspeed *= 1.5f;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            animator.Play("Base Layer.run", -1, 0f);
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            animator.Play("Base Layer.walk", -1, 0f);

        velocity.x = Input.GetAxisRaw("Horizontal") * newspeed;

        if (grounded())
            jumps = 0;

        if ((jumps < maxJumps && Input.GetKeyDown(KeyCode.Space)) || (maxJumps == 0 && grounded()))
        {
            animator.Play("Base Layer.jump", -1, 0f);
            velocity.y = jumpHeight;
            jumps++;
        }

        body.velocity = velocity;
    }


    private bool grounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(hitbox.bounds.center, hitbox.bounds.size, 0, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit2D.collider != null;
    }
}
