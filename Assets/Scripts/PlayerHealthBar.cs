using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player; // Ссылка на игрока
    public Image healthBarImage; // Ссылка на изображение health bar

    void Update()
    {
        // Обновляем ширину health bar в зависимости от текущего здоровья
        float healthPercentage = (float)player.currentHealth / player.maxHealth;
        healthBarImage.fillAmount = healthPercentage; // Используйте fillAmount для заполнения изображения
    }
}
