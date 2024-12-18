using UnityEngine;
using UnityEngine.UI;

public class SecondBossBehaviour : MonoBehaviour, IBoss
{
    public int maxHealth = 1000; // Максимальное здоровье
    private int currentHealth; // Текущее здоровье
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;  

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем текущее здоровье равным максимальному
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0; // Убедитесь, что здоровье не отрицательное
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // Логика второго босса (если нужна)
    }
}
