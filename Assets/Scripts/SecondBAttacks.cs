using UnityEngine;

public class SBAttacks : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject Player;
    public float fireRate = 10f; // ������� ��������
    private float nextFireTime = 0f;
    private float angle1 = 0f;
    public int damage = 10;
    private int current_shoot = 0;// Угол для движения босса
    public float bulletSpeed = 5f; // Скорость снарядов
    public float bulletRadius = 1f; // Радиус круга для движения снарядов
    private float phase1Time = 10f;
    private float pause1Time = 12f;
    private float phase2Time = 17f;
    private float pause2Time = 18f;
    private float phase3Time = 28f;
    private float phaseTimeCurrent = 0f;
    public float moveSpeed = 1f; // Скорость вращения выстрела

    void Update()
    {
        phaseTimeCurrent += 0.01f;
        if (phase1Time >= phaseTimeCurrent)
        {
            angle1 += 15 * Time.deltaTime;
            if (Time.time >= nextFireTime)
            {
                FireBullets();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (pause1Time >= phaseTimeCurrent) 
        {

        }
        else if (phase2Time >= phaseTimeCurrent)
        {
            if (Time.time >= nextFireTime)
            {
                current_shoot += 1;
                FireBullets2();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (pause2Time >= phaseTimeCurrent)
        {

        }
        else if (phase3Time >= phaseTimeCurrent)
        {
            angle1 += 15 * Time.deltaTime;
            if (Time.time >= nextFireTime)
            {
                FireBullets3();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            phaseTimeCurrent = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //          ,                              
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>(); //                    Player
            if (player != null)
            {
                player.TakeDamage(damage); //                    
            }
            Destroy(gameObject); //                                  
        }
    }
    void FireBullets()
    {
        float angleStep = 72f;
        for (int i = 0; i < 5; i++)
        {
            // Вычисляем угол для каждого снаряда
            float angle = angleStep * i - angle1;
            angle += moveSpeed * Time.deltaTime;
            // Создаем снаряд
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            // Задайте параметры для снаряда
        }
    }
    void FireBullets2()
    {
        float angleStep = 30f;
        for (int i = 0; i < 12; i++)
        {
            // Вычисляем угол для каждого снаряда
            float angle = angleStep * i + 15 * current_shoot;
            angle += moveSpeed * Time.deltaTime;
            // Создаем снаряд
            GameObject bullet = Instantiate(bulletPrefab2, transform.position, Quaternion.Euler(0, 0, angle));
            B2Bullet bulletScript = bullet.GetComponent<B2Bullet>();
            if (bulletScript != null)
            {
                bulletScript.speed = bulletSpeed; // Задаем скорость снаряда
            }
        }
    }
    void FireBullets3()
    {
        float angleStep = 72f;
        for (int i = 0; i < 5; i++)
        {
            // Вычисляем угол для каждого снаряда
            float angle = angleStep * i + angle1;
            angle += moveSpeed * Time.deltaTime;
            // Создаем снаряд
            GameObject bullet = Instantiate(bulletPrefab3, transform.position, Quaternion.Euler(0, 0, angle));
            // Задайте параметры для снаряда
        }
    }


}
