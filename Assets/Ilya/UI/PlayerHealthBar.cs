using UnityEngine; 
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour  
{  
    public float maxHealth = 100f;  
    public float currentHealth;  
    public RectTransform healthBarRect; // Ссылка на RectTransform хитбара  
    public float initialBarHeight; // Начальная высота хитбара  

    private float topYPosition; // Верхняя позиция хитбара

    void Start()  
    {  
        currentHealth = maxHealth; // Инициализация текущего здоровья  
        initialBarHeight = healthBarRect.sizeDelta.y; // Сохраняем начальную высоту хитбара

        // Получаем верхнюю позицию хитбара из RectTransform
        topYPosition = healthBarRect.anchoredPosition.y; 

        UpdateHealthBar();  
    }  

    public void TakeDamage(float damage)  
    {  
        currentHealth -= damage;  
        if (currentHealth < 0) currentHealth = 0; // Убедитесь, что здоровье не отрицательное  
        UpdateHealthBar();  
    }  

    private void UpdateHealthBar()   
    {   
        float barHeight = (currentHealth / maxHealth) * initialBarHeight;   
        healthBarRect.sizeDelta = new Vector2(healthBarRect.sizeDelta.x, barHeight);   

        // Устанавливаем верхнюю координату на фиксированное значение
        healthBarRect.anchoredPosition = new Vector2(healthBarRect.anchoredPosition.x, topYPosition);  

        // Если здоровье 0, скрываем хитбар  
        if (currentHealth <= 0)  
        {  
            gameObject.SetActive(false);  
        }  
    } 

    void Update()  
    {  
        // Для тестирования: наносим урон при нажатии пробела  
        if (Input.GetKeyDown(KeyCode.Space))  
        {  
            TakeDamage(10f); // Наносим 10 урона  
        }  
    }  
}
