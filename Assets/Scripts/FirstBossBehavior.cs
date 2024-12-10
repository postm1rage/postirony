using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour  
{  
    public Transform player; // Ссылка на игрока  
    public float minDistance = 5f; // Минимальная дистанция до игрока  
    public float maxDistance = 10f; // Максимальная дистанция до игрока  
    public float moveSpeed = 1.5f; // Скорость передвижения  
    public int maxHealth = 1000; // Максимальное здоровье  
    private int currentHealth; // Текущее здоровье  
    private SpriteRenderer spriteRenderer;
    public Image Healthbar;

    private void Awake()  
    {  
        spriteRenderer = GetComponent<SpriteRenderer>();  
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)  
    {  
        currentHealth -= damage; // Уменьшаем текущее здоровье  

        if (currentHealth <= 0)  
        {  
            Die();  
        }  
        UpdateHealthBar();  
    }
    void UpdateHealthBar()
    {
        float healthRatio = (float)currentHealth / maxHealth;
        Healthbar.fillAmount = healthRatio;
    }
    void Die()  
    {  
        Destroy(gameObject);  
    }  

    void Update()  
    {  
        Vector2 directionToPlayer = (player.position - transform.position).normalized;  

        float currentDistance = Vector2.Distance(transform.position, player.position);  

        if (currentDistance < minDistance)  
        {  
            transform.position -= (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);  
        }  
        else if (currentDistance > maxDistance)  
        {  
            transform.position += (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);  
        }  

        if (transform.position.x < player.position.x)  
        {  
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);  
        }  
        else  
        {  
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);  
        }  
    }  
}
