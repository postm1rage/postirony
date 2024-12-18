using UnityEngine;

public class minigun_Bullet : MonoBehaviour    
{    
    [SerializeField] private int damage = 10;   
    public float lifetime = 2f;    

    void Start()    
    {    
        Destroy(gameObject, lifetime);    
    }    

        void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar healthBar = collision.GetComponent<HealthBar>();
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(damage); // Вызываем TakeDamage у экземпляра Player
                Debug.Log($"Урон игроку: {damage}. Осталось здоровья: {player.currentHealth}");
            }
            else
            {
                Debug.Log("Компонент Player не найден на объекте " + collision.gameObject.name);
            }

            if (healthBar != null)
            {
                healthBar.TakeDamage(damage);  // Наносим урон через HealthBar
            }
            else
            {
                Debug.Log("Компонент HealthBar не найден на объекте " + collision.gameObject.name);
            }

            Destroy(gameObject);
        }
    }

}
