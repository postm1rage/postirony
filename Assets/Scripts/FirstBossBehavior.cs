using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //public Image Healthbar;
    public Transform player; // Ссылка на объект игрока
    public float minDistance = 5f; // Минимальное расстояние до игрока
    public float maxDistance = 10f; // Максимальное расстояние до игрока
    public float moveSpeed = 1.5f; // Скорость движения босса
    //public int maxHealth = 1000; // Максимальное здоровье
    //private int currentHealth; // Текущее здоровье
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /*
    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем текущее здоровье на максимальное значение
        UpdateHealthBar();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье на величину урона

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth; // Обновляем наполненность полосы
    }
    void Die()
    {
        Destroy(gameObject);
    }
    */
    void Update()
    {
        // Вычисляем вектор направления к игроку
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Вычисляем текущее расстояние до игрока
        float currentDistance = Vector2.Distance(transform.position, player.position);

        // Если текущие расстояние меньше минимального
        if (currentDistance < minDistance)
        {
            // Двигаемся прочь от игрока
            transform.position -= (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);
        }
        // Если текущее расстояние больше максимального
        else if (currentDistance > maxDistance)
        {
            // Двигаемся ближе к игроку
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
