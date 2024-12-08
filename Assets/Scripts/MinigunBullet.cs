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
            Player player = collision.GetComponent<Player>(); 

            if (player != null) 
            { 
                player.TakeDamage(damage); 
            } 
            else 
            {
                Debug.Log("Компонент Player не найден на объекте " + collision.gameObject.name); 
            }

            Destroy(gameObject);  
        }  
    }  
}
