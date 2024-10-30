using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;

    [SerializeField] private int speed;
    [SerializeField] private float idleDuration;

    private Vector3 initScale;
    private bool movingLeft;
    private float idleTimer;

    [SerializeField] Animator anim;

    private void Awake()
    {
        initScale = enemy != null ? enemy.localScale : Vector3.one;
    }

    private void OnDisable()
    {
        if (anim != null)
        {
            anim.SetBool("walk", false);
        }
    }

    private void Update()
    {
        if (enemy == null || leftEdge == null || rightEdge == null) return;

        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        if (anim != null)
        {
            anim.SetBool("walk", false);
        }

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        if (anim != null)
        {
            anim.SetBool("walk", true);
        }

        if (enemy != null)
        {
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
        }
    }
}
