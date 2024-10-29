using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(WaitPlayAnim());
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator WaitPlayAnim()
    {
        anim.SetTrigger("dead");
        yield return new WaitForSeconds(2f);

        Die();
    }
}
