using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject Player;
    public GameObject minigun_Bullet;
    public float fireRate = 10f;
    public float projectileSpeed = 10f;
    public float reloadTime = 2f; // ����� �����������
    private int shots = 0;
    private float nextFireTime;
    private bool isReloading = false;
    public float spread = 0.1f;
    private float reloadStartTime;
    public float shakeAmount = 0.1f; // ���� ������
    public float shakeDuration = 0.1f; // ������������ ������

    public AudioSource MinigunShootSound; // ������ �� AudioSource
    public Transform minigun;
    private Vector3 originalPosition;
    public int damage = 10; // ����, ������� ������ �������


    void Start()
    {
        // ��������� �������� ������� ������
        originalPosition = minigun.localPosition;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���������� �� ������ � ������
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>(); // �������� ��������� Player
            if (player != null)
            {
                player.TakeDamage(damage); // ������� ���� ������
            }
            Destroy(gameObject); // ������� ������ ����� ������������
        }
    }
    
    void Update()
    {
        if (isReloading)
        {
            if (Time.time >= reloadStartTime + reloadTime)
            {
                isReloading = false; // ��������� �����������
            }
            return; // ���������� ��������� ��� ���� ���� �����������
        }
        if (Time.time > nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        float randomSpreadX = Random.Range(-spread, spread);
        float randomSpreadY = Random.Range(-spread, spread);

        Vector2 spreadDirection = new Vector2(direction.x + randomSpreadX, direction.y + randomSpreadY).normalized;

        GameObject projectile = Instantiate(minigun_Bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = spreadDirection * projectileSpeed;
        }
        shots++;
        if (MinigunShootSound != null)
        {
            MinigunShootSound.Play();
        }
        StartCoroutine(Recoil());
        if (shots >= 68)
        {
            StartReload();
        }
    }
    private System.Collections.IEnumerator Recoil()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // ��������� ��������� ��������
            Vector3 randomOffset = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0);
            minigun.localPosition = originalPosition + randomOffset; // �������� ������� ������

            elapsed += Time.deltaTime;
            yield return null; // ���� ��������� ����
        }

        minigun.localPosition = originalPosition; // ���������� ������ � �������� ���������
    }
    void StartReload()
    {
        isReloading = true; // ������������� ���� �����������
        reloadStartTime = Time.time; // ��������� ����� ������ �����������
        shots = 0; // ����� �������� ���������
    }
}
