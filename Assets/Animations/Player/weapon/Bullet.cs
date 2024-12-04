using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
   BossBehavior boss = GameObject.Find("Boss").GetComponent<BossBehavior>();
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка на столкновение с врагом
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Наносим урон врагу
                        Boss enemy = collision.gameObject.GetComponent<Boss>();
            if (enemy != null)
            {
                boss.TakeDamage(10);
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}
