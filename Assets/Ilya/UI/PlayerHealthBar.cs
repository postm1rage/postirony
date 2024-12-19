using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Collections;


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
    void Die()
    {
         SceneManager.LoadScene(0);
    }

    private IEnumerator DieCoroutine()
    {
        // Здесь можно добавить анимацию смерти или другие действия
        yield return new WaitForSeconds(1f); // Подождите перед загрузкой
        SceneManager.LoadScene(0);
    }

    public void TakeDamage(float damage)  
    {  
        currentHealth -= damage;  
        if (currentHealth < 0) {
            currentHealth = 0;
            Die();
            } // Убедитесь, что здоровье не отрицательное  
        UpdateHealthBar();  
    }  

    private void UpdateHealthBar()   
    {   
        float barHeight = (currentHealth / maxHealth) * initialBarHeight;   
        healthBarRect.sizeDelta = new Vector2(healthBarRect.sizeDelta.x, barHeight);   

        // Устанавливаем верхнюю координату на фиксированное значение
        healthBarRect.anchoredPosition = new Vector2(healthBarRect.anchoredPosition.x, topYPosition);  
    } 
}
