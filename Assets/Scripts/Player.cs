using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour { 

    public static Player Instance { get; private set; } 

    [SerializeField] private float movingSpeed = 10f; 
    public float dashSpeed = 20f; 
    public float dashLength = .5f; 
    public float dashCooldown = 1f; 

    [SerializeField] private GameObject dashParticlesPrefab; // Префаб системы частиц

    private Rigidbody2D rb; 
    private Vector2 moveInput; 
    private bool isRunning = false; 
    private float dashCounter; 
    private float dashCoolCounter; 

    void Awake() { 
        Instance = this; 
        rb = GetComponent<Rigidbody2D>(); 
    } 

    void Update() { 
        moveInput.x = Input.GetAxisRaw("Horizontal"); 
        moveInput.y = Input.GetAxisRaw("Vertical"); 
        moveInput.Normalize(); 

        if (Input.GetMouseButtonDown(1)) { 
            if (dashCoolCounter <= 0 && dashCounter <= 0) { 
                dashCounter = dashLength; 
                rb.linearVelocity = moveInput * dashSpeed; // Используем даш
                PlayDashParticles(); // Воспроизводим частицы при даше
            } 
        } 

        if (dashCounter > 0) { 
            dashCounter -= Time.deltaTime; 
            if (dashCounter <= 0) { 
                dashCoolCounter = dashCooldown; 
            } 
        } 

        if (dashCoolCounter > 0) { 
            dashCoolCounter -= Time.deltaTime; 
        } 

        HandleMovement(); // Вызываем метод для обработки движения
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

    public Vector3 GetPlayerScreenPosition() { 
        return Camera.main.WorldToScreenPoint(transform.position); 
    } 
}
