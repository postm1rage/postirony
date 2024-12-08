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
