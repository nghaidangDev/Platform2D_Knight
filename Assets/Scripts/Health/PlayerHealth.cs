using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;
    public GameObject deathCV;

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
        else if (health > 0)
        {
            anim.SetTrigger("hit");
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
        deathCV.SetActive(true);
        Die();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }
}
