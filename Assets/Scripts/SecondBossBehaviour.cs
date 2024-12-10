using UnityEngine;
using UnityEngine.UI;

public class SecondBossBehaviour : MonoBehaviour
{
    public Image Healthbar;
    public int maxHealth = 1000; // Максимальное здоровье  
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
            currentHealth = 0;
            // Здесь можно добавить логику для смерти босса
        }
        UpdateHealthBar();
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
