using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player; // Ссылка на игрока
    public RectTransform healthBarRectTransform; // Ссылка на RectTransform полосы здоровья

    private float initialHeight; // Начальная высота полосы здоровья

    void Start()
    {
        // Сохраняем начальную высоту полосы здоровья
        initialHeight = healthBarRectTransform.sizeDelta.y;

        // Убедитесь, что здоровье игрока в пределах допустимого диапазона
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
    }

    void Update()
    {
        // Проверяем, чтобы текущее здоровье было в пределах допустимого диапазона
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
        
        // Рассчитываем процент здоровья
        float healthPercentage = (float)player.currentHealth / player.maxHealth;

        // Обновляем высоту полосы здоровья
        Vector2 size = healthBarRectTransform.sizeDelta;
        size.y = initialHeight * (1 - healthPercentage); // Высота уменьшается пропорционально текущему здоровью
        
        // Убедимся, что высота не меньше 0
        size.y = Mathf.Max(size.y, 0);
        
        healthBarRectTransform.sizeDelta = size; // Применяем новые размеры
    }
}
