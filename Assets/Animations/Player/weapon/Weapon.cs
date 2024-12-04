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
    [SerializeField] private SpriteRenderer weaponSprite; 
    [SerializeField] private AudioSource audioSource; // Ссылка на AudioSource

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
        else if (!Input.GetButton("Fire1") && animator.GetBool("IsShooting")) 
        {
            animator.SetBool("IsShooting", false); 
        }

        // Stop shooting animation if no ammo
        if (currentAmmo <= 0 && animator.GetBool("IsShooting"))
        {
            animator.SetBool("IsShooting", false);
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading) 
        { 
            StartCoroutine(Reload()); 
        } 
    }

    void Aim()  
    {  
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        mousePosition.z = 0; 

        Vector3 direction = (mousePosition - transform.position).normalized;  
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 

        firePoint.right = direction;

        if (mousePosition.x < transform.position.x) 
        {
            weaponSprite.flipX = true; 
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, angle + 180); 
        } 
        else 
        {
            weaponSprite.flipX = false; 
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, angle); 
        }
    }

    void Shoot()    
    {    
        if (currentAmmo > 0)    
        {    
            currentAmmo--;    
            audioSource.PlayOneShot(shootSound); // Заменяем на PlayOneShot
            animator.SetTrigger("IsShooting");  
            animator.SetBool("IsShooting", true);  
  
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);    
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            Vector2 directionToMouse = (mousePosition - firePoint.position).normalized;

            rb.linearVelocity = directionToMouse * bulletSpeed;

            nextFireTime = Time.time + fireRate;   
        }
        else 
        {
            animator.SetBool("IsShooting", false); // Stop shooting animation if no ammo
        }    
    }

    private IEnumerator Reload() 
    { 
        isReloading = true; 
        audioSource.PlayOneShot(reloadSound); // Заменяем на PlayOneShot
        yield return new WaitForSeconds(reloadTime); 
        currentAmmo = maxAmmo; 
        isReloading = false; 
    } 
}
