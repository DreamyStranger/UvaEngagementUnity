using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public LayerMask ground;
    public float speedX;
    public float speedY;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bx;
    private Animator animator;

    private float dirX;
    private bool jump;

    private bool onGround;
    private int state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Intersection();
        dirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump = true;
        }

    }

    private void FixedUpdate()
    {
        HandleMovement();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        rb.velocity = new Vector2(dirX * speedX, rb.velocity.y);
        if (jump)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, speedY);
        }
    }

    private void UpdateAnimation()
    {
        if (dirX != 0)
        {
            transform.localScale= new Vector3(0.2f * dirX, 0.2f, 1);
        }

        if (rb.velocity.y != 0f)
        {
            state = 2;
        }
        else
        {
            if (rb.velocity.x != 0f)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }
        }
        animator.SetInteger("state", state);
    }

    private bool Intersection()
    {
        bool intersects = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size, 0f, Vector2.down, 0.1f, ground);
        return intersects;
    }
}
