using UnityEngine;
using TMPro; // Не забудьте подключить TextMeshPro

public class AmmoUIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText; // Ссылка на TextMeshPro компонент
    public Weapon weapon;  // Ссылка на Weapon скрипт

    void Update()
    {
        // Обновление текста в зависимости от состояния
        if (weapon.isReloading)
        {
            ammoText.text = "RELOADING";
        }
        else
        {
            ammoText.text = $"{weapon.currentAmmo} / {weapon.maxAmmo}";
        }
    }
}
