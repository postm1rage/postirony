using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //public Image Healthbar;
    public Transform player; // ������ �� ������ ������
    public float minDistance = 5f; // ����������� ���������� �� ������
    public float maxDistance = 10f; // ������������ ���������� �� ������
    public float moveSpeed = 1.5f; // �������� �������� �����
    //public int maxHealth = 1000; // ������������ ��������
    //private int currentHealth; // ������� ��������
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /*
    void Start()
    {
        currentHealth = maxHealth; // ������������� ������� �������� �� ������������ ��������
        UpdateHealthBar();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� �������� �� �������� �����

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth; // ��������� ������������� ������
    }
    void Die()
    {
        Destroy(gameObject);
    }
    */
    void Update()
    {
        // ��������� ������ ����������� � ������
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // ��������� ������� ���������� �� ������
        float currentDistance = Vector2.Distance(transform.position, player.position);

        // ���� ������� ���������� ������ ������������
        if (currentDistance < minDistance)
        {
            // ��������� ����� �� ������
            transform.position -= (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);
        }
        // ���� ������� ���������� ������ �������������
        else if (currentDistance > maxDistance)
        {
            // ��������� ����� � ������
            transform.position += (Vector3)(directionToPlayer * moveSpeed * Time.deltaTime);
        }
        if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

    }

}
