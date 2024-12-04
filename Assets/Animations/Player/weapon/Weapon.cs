using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{ 
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float bulletSpeed = 20f; 
    [SerializeField] private int maxAmmo = 30; 
    [SerializeField] private float reloadTime = 1.5f; 
    [SerializeField] private float fireRate = 0.5f; 
    [SerializeField] private AudioClip shootSound; 
    [SerializeField] private AudioClip reloadSound; 
    [SerializeField] private Animator animator; 
    [SerializeField] private SpriteRenderer weaponSprite; // Ссылка на SpriteRenderer оружия

    private int currentAmmo; 
    private bool isReloading = false; 
    private float nextFireTime = 0f; 

    void Start() 
    { 
        currentAmmo = maxAmmo; 
    } 

    void Update() 
    { 
        Aim(); 

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !isReloading) 
        { 
            Shoot(); 
        }
        else if (!Input.GetButton("Fire1") && animator.GetBool("IsShooting")) // Добавлено условие
        {
            animator.SetBool("IsShooting", false); // Сбрасываем состояние стрельбы
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading) 
        { 
            StartCoroutine(Reload()); 
        } 
    }


    void Aim()  
    {  
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        mousePosition.z = 0; // Обнуление Z координаты для 2D пространства

        Vector3 direction = (mousePosition - transform.position).normalized;  

        // Вычисляем угол для поворота
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 

        // Устанавливаем направление стрельбы в firePoint
        firePoint.right = direction;

        // Поворачиваем и инвертируем спрайт оружия
        if (mousePosition.x < transform.position.x) 
        {
            weaponSprite.flipX = true; // Инвертируем спрайт по X
            // Поворачиваем на 180 градусов
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, angle + 180); 
        } 
        else 
        {
            weaponSprite.flipX = false; // Возвращаем в нормальное состояние
            // Убираем поворот
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, angle); 
        }
    }

    void Shoot()    
{    
    if (currentAmmo > 0)    
    {    
        currentAmmo--;    
        AudioSource.PlayClipAtPoint(shootSound, transform.position);    
        animator.SetTrigger("IsShooting"); // Установите триггер на стреление  
        animator.SetBool("IsShooting", true); // Устанавливаем состояние стрельбы  
  
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);    
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    

        // Получаем позицию курсора
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Обнуляем Z координату для 2D пространства

        // Вычисляем направление к курсору
        Vector2 directionToMouse = (mousePosition - firePoint.position).normalized;

        // Устанавливаем скорость пули
        rb.linearVelocity = directionToMouse * bulletSpeed;

        nextFireTime = Time.time + fireRate;   
    }    
}
    private IEnumerator Reload() 
    { 
        isReloading = true; 
        AudioSource.PlayClipAtPoint(reloadSound, transform.position); 
        yield return new WaitForSeconds(reloadTime); 
        currentAmmo = maxAmmo; 
        isReloading = false; 
    } 
}
