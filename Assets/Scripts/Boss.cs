using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject Player;
    public GameObject minigun_Bullet;
    public float fireRate = 0.3f; 
    public float projectileSpeed = 10f;

    private float nextFireTime;

    void Update()
    {
        
        if (Time.time > nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(minigun_Bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
