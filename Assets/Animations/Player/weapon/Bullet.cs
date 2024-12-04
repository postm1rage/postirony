using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    private BossBehavior boss; // Declare BossBehavior variable

    // Use Awake to find the Boss game object
    void Awake()
    {
        // Find the Boss object and get the BossBehavior component
        GameObject bossObject = GameObject.Find("Boss");
        if (bossObject != null)
        {
            boss = bossObject.GetComponent<BossBehavior>();
        }
        else
        {
            Debug.LogError("Boss not found in the scene!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    // Check for collision with the enemy
    if (collision.gameObject.CompareTag("Boss"))
    {
        // Теперь используем BossBehavior
        BossBehavior bossBehavior = collision.gameObject.GetComponent<BossBehavior>();
        if (bossBehavior != null)
        {
            // Вызываем метод TakeDamage у BossBehavior
            bossBehavior.TakeDamage(damage);
        }
        Destroy(gameObject); // Destroy the bullet
    }
}
}
