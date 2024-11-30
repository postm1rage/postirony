using System.Collections.Generic; 
using UnityEngine; 

public class Player : MonoBehaviour { 

    public static Player Instance { get; private set; } 

    [SerializeField] private float movingSpeed = 10f; 
    [SerializeField] private GameObject dashParticlesPrefab; // Префаб системы частиц

    public float dashSpeed = 20f; // Убедитесь, что значение больше movingSpeed
    public float dashLength = .5f; 
    public float dashCooldown = 1f; 

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

    private void PlayDashParticles() {
    if (dashParticlesPrefab != null) {
        Instantiate(dashParticlesPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate() { 
        isRunning = moveInput.magnitude > 0.1f;
    } 

    private void HandleMovement() {
        // Если игрок не в состоянии даша, перемещаем его с обычной скоростью
        if (dashCounter <= 0) {
            // Перемещаем игрока с использованием rb.MovePosition
            rb.MovePosition(rb.position + moveInput * (movingSpeed * Time.fixedDeltaTime));
        }
    }

    public bool IsRunning() { 
        return isRunning; 
    } 

    public Vector3 GetPlayerScreenPosition() { 
        return Camera.main.WorldToScreenPoint(transform.position); 
    } 
}
