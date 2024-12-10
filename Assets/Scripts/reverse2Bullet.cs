using UnityEngine;

public class RBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public float speed = -15f; // Скорость снаряда
    public float radius = 1f; // Радиус круга, по которому будет двигаться снаряд
    private float angle = 0f; // Начальный угол
    public float lifetime = 6f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar healthBar = collision.GetComponent<HealthBar>();

            if (healthBar != null)
            {
                healthBar.TakeDamage(damage);  // Наносим урон через HealthBar
            }
            else
            {
                Debug.Log("Компонент HealthBar не найден на объекте " + collision.gameObject.name);
            }

            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Инициализируем угол с учетом положения снаряда
        angle = transform.eulerAngles.z;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        radius += 0.02f;
        // Увеличиваем угол с каждым кадром
        angle += speed * Time.deltaTime;

        // Вычисляем новое положение снаряда по кругу
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius - 0.5f;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius - 0.3f;

        // Устанавливаем новое положение снаряда на основе угла
        transform.localPosition = new Vector3(x, y, 0);
    }

}
