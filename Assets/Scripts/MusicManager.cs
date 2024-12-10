using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр MusicManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Уничтожаем новый объект, если уже есть существующий
            return;
        }

        // Устанавливаем текущий объект как единственный экземпляр
        instance = this;
        DontDestroyOnLoad(gameObject); // Не уничтожать объект при смене сцены
    }
}