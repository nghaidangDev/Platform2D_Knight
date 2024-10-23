using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float coolDownTimer = 0f;


    [SerializeField] private float speed;
    [SerializeField] private float forceJump;
    [SerializeField] private float attackTimer;

    [SerializeField] private float checkRadiousGrounded;
    [SerializeField] private float checkRadiousAttacked;

    private bool isGrounded;
    private bool isFacingRight;
    private bool canJumpDouble;

    [SerializeField] private Transform checkGround;
    [SerializeField] private Transform checkAttack;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

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
        CanJump();
        CanAttack();
        UpdateAnimations();
        CheckAnyThing();
        Flip();
    }

    private void UpdateAnimations()
    {
        anim.SetBool("move", horizontal != 0);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void CanMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void CanJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, forceJump);
        }
    }

    private void CanAttack()
    {
        if (coolDownTimer <= 0)
        {
            if (Input.GetMouseButton(0) && isGrounded)
            {
                Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(checkAttack.position, checkRadiousAttacked, enemyLayer);

                foreach (Collider2D collider in enemyInRange)
                {
                    //Take damage
                }
                Debug.Log("Attack");
                coolDownTimer = attackTimer;
                anim.SetTrigger("attack");
            }
        }
        else
        {
            coolDownTimer -= Time.deltaTime;

            if (coolDownTimer <= 0)
            {
                coolDownTimer = 0;
            }
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

    private void CheckJumpDouble()
    {
        if (!isGrounded && Input.GetButtonDown("Jump"))
            canJumpDouble = true;
    }

    private void CheckAnyThing()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkRadiousGrounded, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkGround.position, checkRadiousGrounded);

        Gizmos.DrawSphere(checkAttack.position, checkRadiousAttacked);
    }
}
