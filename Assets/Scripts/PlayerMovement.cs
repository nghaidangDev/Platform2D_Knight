using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator anim;

    private float horizontal;
    private float vertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
        Attack();  
    }

    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        anim.SetBool("walk_front", vertical == -1);
        anim.SetBool("walk_back", vertical == 1);
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("attack_front");
        }
    }
}
