using System.Collections;  
using UnityEngine;  

public class Player : MonoBehaviour {   

    public static Player Instance { get; private set; }   

    [SerializeField] private float movingSpeed = 10f;   
    public float dashSpeed = 20f;   
    public float dashLength = .5f;
    public float dashCooldown = 1f;   

    [SerializeField] private GameObject dashParticlesPrefab; // Префаб системы частиц  
    [SerializeField] private AudioClip dashSound; // Аудиоклип для даша 
    [SerializeField] private AudioClip walkSound; // Аудиоклип для звука шагов 
    private AudioSource audioSource; // Компонент AudioSource 

    private Rigidbody2D rb;   
    private Vector2 moveInput;   
    private bool isRunning = false;   
    private bool isDashing = false; // Переменная для отслеживания состояния даша
    private float dashCounter;   
    private float dashCoolCounter;   
    private float walkSoundTimer = 0.0f; // Таймер для звука шагов 
    public float walkSoundInterval = 0.5f; // Интервал воспроизведения звука шагов 

    void Awake() {   
        Instance = this;   
        rb = GetComponent<Rigidbody2D>();   
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource 
    }   

    void Update() {   
        moveInput.x = Input.GetAxisRaw("Horizontal");   
        moveInput.y = Input.GetAxisRaw("Vertical");   
        moveInput.Normalize();   

        if (Input.GetMouseButtonDown(1)) {   
            if (dashCoolCounter <= 0 && dashCounter <= 0) {   
                isDashing = true; // Устанавливаем состояние даша
                dashCounter = dashLength;   
                rb.linearVelocity = moveInput * dashSpeed; // Используем даш  
                PlayDashParticles(); // Воспроизводим частицы при даше 
                PlayDashSound(); // Воспроизводим звук даша 
            }   
        }   

        if (dashCounter > 0) {   
            dashCounter -= Time.deltaTime;   
            if (dashCounter <= 0) {   
                dashCoolCounter = dashCooldown;   
                isDashing = false; // Сбрасываем состояние даша
            }   
        }   

        if (dashCoolCounter > 0) {   
            dashCoolCounter -= Time.deltaTime;   
        }   

        HandleMovement(); // Вызываем метод для обработки движения  
        HandleWalkingSound(); // Вызываем метод для обработки звука шагов 
    }   

    private void FixedUpdate() {   
        isRunning = moveInput.magnitude > 0.1f;  
    }   

    private void HandleMovement() {  
        if (dashCounter <= 0) {  
            Vector2 targetPosition = rb.position + moveInput * (movingSpeed * Time.fixedDeltaTime);  
              
            // Проверяем наличие препятствий  
            RaycastHit2D hit = Physics2D.Raycast(rb.position, moveInput, moveInput.magnitude * movingSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Obstacles"));  

            if (hit.collider == null) {  
                // Если нет столкновений, перемещаем игрока  
                rb.MovePosition(targetPosition);  
            }  
        } else {  
            // Для даша можно добавить аналогичную проверку  
            Vector2 dashTargetPosition = rb.position + moveInput * (dashSpeed * Time.fixedDeltaTime);  
            RaycastHit2D dashHit = Physics2D.Raycast(rb.position, moveInput, moveInput.magnitude * dashSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Obstacles"));  

            if (dashHit.collider == null) {  
                // Если нет столкновений, выполняем даш  
                rb.MovePosition(dashTargetPosition);  
            }  
        }  
    }  

    private void PlayDashParticles() {  
        if (dashParticlesPrefab != null) { 
            GameObject particles = Instantiate(dashParticlesPrefab, transform.position, Quaternion.identity);
                        StartCoroutine(DestroyParticles(particles)); // Запускаем корутину для удаления частиц  
        }  
    }  

    private void PlayDashSound() { 
        if (dashSound != null && audioSource != null) { 
            audioSource.PlayOneShot(dashSound); // Воспроизводим звук даша 
        } 
    } 

    private void HandleWalkingSound() { 
        if (isRunning) {
            walkSoundTimer += Time.deltaTime;
            if (walkSoundTimer >= walkSoundInterval) {
                PlayWalkSound();
                walkSoundTimer = 0.0f;
            }
        } else {
            walkSoundTimer = 0.0f; // Сбрасываем таймер, если не двигаемся
        }
    }

    private void PlayWalkSound() {
        if (walkSound != null && audioSource != null) {
            audioSource.PlayOneShot(walkSound); // Воспроизводим звук шагов
        }
    }

    private IEnumerator DestroyParticles(GameObject particles) { 
        var particleSystem = particles.GetComponent<ParticleSystem>(); 
        if (particleSystem != null) { 
            // Ждем, пока система частиц завершит свою работу 
            yield return new WaitForSeconds(particleSystem.main.duration); 
        } 
        // Уничтожаем объект системы частиц 
        Destroy(particles); 
    } 

    public bool IsRunning() {  
        return isRunning;  
    }  

    public bool IsDashing() {
        return isDashing;
    }

    public Vector3 GetPlayerScreenPosition() {  
        return Camera.main.WorldToScreenPoint(transform.position);  
    }  
}
