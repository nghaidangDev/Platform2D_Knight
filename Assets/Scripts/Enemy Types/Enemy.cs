﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radiousInRange;
    [SerializeField] private float attackTimer;
    [SerializeField] private float damaged;

    private float nearDistance = 0.2f;
    private float coolDownTimer = 0f;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 target;

    [SerializeField] private Transform attackGround;
    [SerializeField] private LayerMask playerLayer;

    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isFacingRight = true;

    private Animator anim;

    private void Start()
    {
        GenerateRandomPoints();

        target = pointB;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isWalking)
        {
            EnemyMovement();
        }

        if (isAttacking)
        {
            Attacking();
        }
    }

    private void GenerateRandomPoints()
    {
        pointA = new Vector3(0.5f, -7.2f);
        pointB = new Vector3(6.5f, -7.2f);
    }

    private void EnemyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < nearDistance)
        {
            StartCoroutine(WaitForFlip());
        }

        anim.SetBool("walk", isWalking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            Attacking();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            coolDownTimer = 0f;
        }
    }

    private void Attacking()
    {
        if (coolDownTimer <= 0)
        {
            Collider2D[] playerInRange = Physics2D.OverlapCircleAll(attackGround.position, radiousInRange, playerLayer);

            foreach (var player in playerInRange)
            {
                player.GetComponent<Health>().TakeDamage(damaged);
            }

            coolDownTimer = attackTimer;
            anim.SetBool("Attack", isAttacking);
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

    private void FLip()
    {
        // Kiểm tra hướng của target so với vị trí hiện tại
        bool shouldFaceRight = target.x > transform.position.x;

        if (shouldFaceRight != isFacingRight)
        {
            isFacingRight = shouldFaceRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    IEnumerator WaitForFlip()
    {
        isWalking = true;

        yield return new WaitForSeconds(2f);

        target = target == pointA ? pointB : pointA;
        FLip();

        isWalking = false;
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA, 0.2f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pointB, 0.2f);
        }

        Gizmos.DrawSphere(attackGround.position, radiousInRange);
    }
}
