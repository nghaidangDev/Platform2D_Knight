using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    private float horizontal;
    private bool isFacingRight;
    private bool isJumping;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerMove();
        Jump();
        Flip();
    }

    private void PlayerMove()
    {
        horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);

        anim.SetBool("walk", horizontal != 0);

    }

    private void Flip()
    {
        if (isFacingRight && horizontal > 0 || !isFacingRight && horizontal < 0)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if (!Input.GetKey(KeyCode.Space) && isGrounded())
        {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded() || isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetTrigger("jump");

                if (isJumping)
                {
                    anim.SetTrigger("jump");
                }

                isJumping = !isJumping;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    public bool canAttack()
    {
        return horizontal == 0 && isGrounded();
    }
}
