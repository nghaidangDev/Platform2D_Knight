using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("move");

            if (anim != null)
            {
                anim.SetTrigger("exploded");
                HealthBarUI.instance.TakeDamage(20);
            }

            if (HealthBarUI.instance.currentHealth <= 0)
            {
                PlayerMovement.Destroy(gameObject);
            }
        }
    }
}
