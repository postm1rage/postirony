using UnityEngine; 

public class Bullet : MonoBehaviour
{ 
    public int damage = 10; 

    void Start() 
    { 
        Destroy(gameObject, 2f); 
    } 

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        // Проверяем столкновение с боссами
        IBoss bossBehavior = collision.gameObject.GetComponent<IBoss>();

        if (bossBehavior != null) 
        { 
            // Вызываем метод TakeDamage у соответствующего поведения босса 
            bossBehavior.TakeDamage(damage); 
            Destroy(gameObject); // Удаляем пулю 
        } 
    } 
}
