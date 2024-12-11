using UnityEngine;

public class B2Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public float lifetime = 2f;
    public float speed = 2f; // �������� �������
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
        // ������������� �������� ������� ������ � ����������� ��� "������"
        rb.linearVelocity = transform.up * speed; // ����������(Direction)
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
    }
}
