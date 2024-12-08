using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DashBar : MonoBehaviour
{
    public float returnDuration = 1f; // Длительность возвращения бара
    public RectTransform dashBarRect; // Ссылка на RectTransform бара даша
    private float initialBarHeight; // Начальная высота даша
    private bool isInCooldown = false; // Флаг для отслеживания состояния КД
    private bool isDashingActive = false; // Флаг для отслеживания состояния даша
    private Player player; // Ссылка на компонент Player

    public bool IsDashing { get; set; } // Триггер для проверки состояния даша

    private void Start()
    {
        initialBarHeight = dashBarRect.sizeDelta.y; // Сохраняем начальную высоту даша
        UpdateDashBar(); // Обновляем бар в начале
        player = Player.Instance; // Получаем ссылку на игрока
    }

    public void StartDash()
{
    // Проверяем, в состоянии ли даш игрока или даш уже в процессе отката
    if (player.isDashing || isInCooldown) return;

    isDashingActive = true; // Устанавливаем флаг активности даша
    IsDashing = true; // Устанавливаем триггер в true

    // Резкое уменьшение до нуля
    dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, 0);
    isInCooldown = true; // Устанавливаем, что начат откат
    StartCoroutine(ReturnDashBar());
}

    private IEnumerator ReturnDashBar()
    {
        // Плавное возвращение бара к начальному размеру
        isInCooldown = true; // Устанавливаем, что начат откат
        float elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float newHeight = Mathf.Lerp(0, initialBarHeight, elapsedTime / returnDuration);
            dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, newHeight);
            yield return null; // Ждем один кадр
        }
        
        // Убедимся, что бар точно вернулся к начальному размеру
        dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, initialBarHeight);
        isInCooldown = false; // Сбрасываем флаг КД после завершения
        isDashingActive = false; // Сбрасываем флаг активности даша
    }

    private void UpdateDashBar()
    {
        dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, initialBarHeight);
    }

    private void Update()
    {
        // Для тестирования: начинаем даш при нажатии ПКМ
        if (Input.GetMouseButtonDown(1)) // 1 - это ПКМ
        {
            StartDash(); // Начинаем даш
        }

        // Для тестирования: останавливаем даш при отпускании ПКМ
        if (Input.GetMouseButtonUp(1)) // 1 - это ПКМ
        {
            IsDashing = false; // Устанавливаем триггер в false
        }
    }
}
