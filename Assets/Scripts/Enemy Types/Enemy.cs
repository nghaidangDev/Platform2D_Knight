using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radiousInRange;
    [SerializeField] private float attackTimer;
    [SerializeField] private int damaged;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            coolDownTimer = 0f;
        }
    }

    private void Attacking()
    {
        if (coolDownTimer <= 0)
        {
            Collider2D playerInRange = Physics2D.OverlapCircle(attackGround.position, radiousInRange, playerLayer);

            if (playerInRange != null)
            {
                anim.SetTrigger("attack"); // Chỉ bắt đầu hoạt ảnh tấn công
            }

            coolDownTimer = attackTimer;
        }
        else
        {
            coolDownTimer -= Time.deltaTime;
        }
    }

    // Phương thức này sẽ được gọi từ Animation Event
    private void TriggerDamage()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(attackGround.position, radiousInRange, playerLayer);

        if (playerInRange != null)
        {
            playerInRange.GetComponent<PlayerHealth>().TakeDamage(damaged);
        }
    }

    private void FLip()
    {
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
