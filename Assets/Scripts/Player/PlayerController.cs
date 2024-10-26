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
    private bool canMove = true;

    [SerializeField] private Transform checkGround;
    [SerializeField] private Transform checkAttack;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Animator anim;
    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


        if (rb == null) Debug.LogError("Rigidbody2D is missing!");
        if (anim == null) Debug.LogError("Animator is missing!");
        if (audioManager == null) Debug.LogError("AudioManager is missing!");
        if (checkGround == null || checkAttack == null) Debug.LogError("CheckGround or CheckAttack is missing!");

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

        if (canMove)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            FindObjectOfType<AudioManager>().Play("Walk");
        }
    }

    private void CanJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, forceJump);
            FindObjectOfType<AudioManager>().Play("Jump");
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
                FindObjectOfType<AudioManager>().Play("Attack");
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

    public void DisableMove()
    {
        canMove = false;
    }

    public void EnableMove()
    {
        canMove = true;
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
