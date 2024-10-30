using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = enemyHealth.health;
    }

    private void Update()
    {
        slider.value = enemyHealth.health;
    }
}
