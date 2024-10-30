using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal;

    [SerializeField] private float speed;
    [SerializeField] private float forceJump;

    [SerializeField] private float checkRadiousGrounded;

    private bool isGrounded;
    private bool isFacingRight;
    //private bool canJumpDouble;
    private bool canMove = true;

    [SerializeField] private Transform checkGround;
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
        if (checkGround == null) Debug.LogError("CheckGround is missing!");
    }


    private void Update()
    {
        CanMovement();
        CanJump();
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
        //if (!isGrounded && Input.GetButtonDown("Jump"))
            //canJumpDouble = true;
    }

    private void CheckAnyThing()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkRadiousGrounded, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkGround.position, checkRadiousGrounded);
    }

    public bool IsCanAttack()
    {
        return horizontal == 0 && isGrounded;
    }
}
