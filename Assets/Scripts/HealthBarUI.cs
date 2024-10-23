using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image imgHealthUI;

    private void Awake()
    {
        if (imgHealthUI != null)
        {
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        float fillamount = Health.instance.currentHealth / Health.instance.startingHealth;
        imgHealthUI.fillAmount = fillamount;
    }
}
