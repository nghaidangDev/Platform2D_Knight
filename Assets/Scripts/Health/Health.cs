using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damaged)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damaged, 0, startingHealth);
    }
}
