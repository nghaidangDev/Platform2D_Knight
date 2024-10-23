using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;

    public float startingHealth;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        startingHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
}
