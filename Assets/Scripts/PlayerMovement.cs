using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool notfinish = true;
    [SerializeField] private LayerMask jumpableGround;
    private bool m_FacingRight = true;
    private float dirX=0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;


    private enum MovementState { idle, Running , jumping , falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
   private void Update()
    {
        
         dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX* moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded() )
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (notfinish)
        {
            if (dirX > 0f)
            {
                state = MovementState.Running;
                //sprite.flipX = false;
                if (!m_FacingRight)
                {
                    Flip();
                }
            }
            else if (dirX < 0f)
            {
                state = MovementState.Running;
                // sprite.flipX = true;
                if (m_FacingRight)
                {
                    Flip();
                }
            }
            else
            {
                state = MovementState.idle;
            }
            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -1f)
            {
                state = MovementState.falling;

            }
            anim.SetInteger("state", (int)state);
        }
       
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.  
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }
    private void Finish()
    {
        notfinish = false;
        anim.SetTrigger("Idle");
        moveSpeed = 0f;
        jumpForce = 0f;

    }
}