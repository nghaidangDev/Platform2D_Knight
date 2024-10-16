using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public static HealthBarUI instance;
    [SerializeField] private Image currentHealthImg;
    public float currentHealth = 100;

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
    }

    private void Update()
    {
        UpdateCurrentHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateCurrentHealth();
    }

    private void UpdateCurrentHealth()
    {
        currentHealthImg.fillAmount = currentHealth / 100;
    }
}
