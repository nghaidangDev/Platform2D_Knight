using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public static HealthBarUI instance;

    [SerializeField] private Image imgHealthUI;

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

        if (imgHealthUI != null)
        {
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        if (Health.instance != null)
        {
            float fillamount = Health.instance.currentHealth / Health.instance.startingHealth;
            imgHealthUI.fillAmount = fillamount;
        }
    }
}
