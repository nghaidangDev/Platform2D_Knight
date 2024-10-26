using UnityEngine;

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
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        HealthBarUI.instance.UpdateHealthUI();  // Cập nhật UI sau khi giảm sức khỏe
        FindObjectOfType<AudioManager>().Play("Hit");

        if (currentHealth == 0)
        {
            FindObjectOfType<AudioManager>().Play("Deaded");
            GetComponent<PlayerController>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
