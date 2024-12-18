using UnityEngine;

public class BBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public float speed = 15f; // �������� �������
    public float radius = 1f; // ������ �����, �� �������� ����� ��������� ������
    private float angle = 0f; // ��������� ����
    public float lifetime = 6f;


    void Start()
    {
        // �������������� ���� � ������ ��������� �������
        angle = transform.eulerAngles.z;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar healthBar = collision.GetComponent<HealthBar>();

            if (healthBar != null)
            {
                healthBar.TakeDamage(damage);  // ������� ���� ����� HealthBar
            }
            else
            {
                Debug.Log("��������� HealthBar �� ������ �� ������� " + collision.gameObject.name);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Если пуля столкнулась с объектом со сценой с тегом "Default"
            Destroy(gameObject); // Удаляем пулю
        }
    }

    void Update()
    {
        radius += 0.02f;
        // ����������� ���� � ������ ������
        angle += speed * Time.deltaTime;

        // ��������� ����� ��������� ������� �� �����
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius - 0.5f;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius - 0.3f;

        // ������������� ����� ��������� ������� �� ������ ����
        transform.localPosition = new Vector3(x, y, 0);
    }

}
