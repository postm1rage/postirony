using UnityEngine;
using UnityEngine.UI;

public class SecondBossBehaviour : MonoBehaviour, IBoss
{
    public Image Healthbar;
    public int maxHealth = 10; // Максимальное здоровье  
    private float currentHealth; // Текущее здоровье  
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        float healthRatio = (float)currentHealth / maxHealth;
        Healthbar.fillAmount = healthRatio;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
