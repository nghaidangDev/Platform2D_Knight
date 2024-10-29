using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = playerHealth.health;
    }

    private void Update()
    {
        slider.value = playerHealth.health;
    }
}
