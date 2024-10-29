using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private int damage;
    private Health health;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision !=  null)
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
