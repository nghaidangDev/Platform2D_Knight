using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;

    [SerializeField] private float speed;

    [SerializeField] private float checkRadiousGrounded;

    private bool isGrounded;
    private bool isFacingRight;
    private bool canJump;

    [SerializeField] private Transform checkGround;
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
        CanMovement();
        UpdateAnimations();
        Flip();
    }

    private void Start()
    {
        CheckAnyThing();
    }

    private void UpdateAnimations()
    {
        anim.SetBool("move", horizontal != 0);
    }

    private void CanMovement()
    {
        canJump = true;
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal > 0 || !isFacingRight && horizontal < 0)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void CheckAnyThing()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkRadiousGrounded, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkGround.position, checkRadiousGrounded);
    }
}
