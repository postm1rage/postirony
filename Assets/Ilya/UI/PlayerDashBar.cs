using UnityEngine; 
using UnityEngine.UI; 
using System.Collections; 
 
public class DashBar : MonoBehaviour 
{ 
    public float returnDuration = 1f; // Длительность возвращения бара 
    public RectTransform dashBarRect; // Ссылка на RectTransform бара даша 
    private float initialBarHeight; // Начальная высота даша 
    private bool isInCooldown = false; // Флаг для отслеживания состояния КД 
    private Player player; // Ссылка на компонент Player 
 
    public bool IsDashing { get; set; } // Триггер для проверки состояния даша 
 
    [SerializeField] 
    private float cooldownTime = 2f; // Время между нажатиями ПКМ 
    private float lastClickTime = 0f; // Время последнего нажатия 
 
    private void Start() 
    { 
        initialBarHeight = dashBarRect.sizeDelta.y; // Сохраняем начальную высоту даша 
        UpdateDashBar(); // Обновляем бар в начале 
        player = Player.Instance; // Получаем ссылку на игрока 
    } 
 
    public void StartDash() 
    { 
        if (isInCooldown) return; // Если в состоянии КД, выходим из метода
 
        isInCooldown = true; // Устанавливаем, что начат откат
        IsDashing = true; // Устанавливаем триггер в true 

        // Резкое уменьшение до нуля 
        dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, 0); 
        StartCoroutine(ReturnDashBar()); 
    } 
 
    private IEnumerator ReturnDashBar() 
    { 
        // Плавное возвращение бара к начальному размеру 
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
    } 
 
    private void UpdateDashBar() 
    { 
        dashBarRect.sizeDelta = new Vector2(dashBarRect.sizeDelta.x, initialBarHeight); 
    } 
 
    private void Update() 
    { 
        // Проверяем, прошло ли достаточно времени с последнего нажатия ПКМ
        if (Input.GetMouseButtonDown(1)) // 1 - это ПКМ
        {
            if (Time.time >= lastClickTime + cooldownTime) // Проверяем КД
            {
                lastClickTime = Time.time; // Обновляем время последнего нажатия
                StartDash(); // Начинаем даш
            }
        }
    } 
}
