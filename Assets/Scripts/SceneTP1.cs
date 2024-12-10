using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportWithLoadingScreen : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "FirstRoom"; // Имя целевой сцены
    [SerializeField] private GameObject loadingScreenPrefab; // Префаб спрайта загрузочного экрана
    private GameObject loadingScreenInstance; // Экземпляр загрузочного экрана
    private bool isInputBlocked = false; // Флаг блокировки ввода

    private void Update()
    {
        // Блокируем все действия, если активен экран загрузки
        if (isInputBlocked)
        {
            // Блокируем любую активность ввода (допустим, через CursorLock или в UI)
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, входит ли объект с тегом "Player" в триггер
        Transform rootObject = GetRootObjectWithTag(other.gameObject, "Player");
        if (rootObject != null)
        {
            Debug.Log($"Player detected! Preparing to teleport to scene {targetSceneName}");

            // Запускаем корутину для отображения экрана загрузки и телепортации
            StartCoroutine(ShowLoadingScreenAndTeleport(rootObject));
        }
    }

    // Корутин для отображения экрана загрузки и телепортации
    private System.Collections.IEnumerator ShowLoadingScreenAndTeleport(Transform player)
    {
        // Активируем блокировку ввода
        isInputBlocked = true;

        // Создаём экран загрузки
        if (loadingScreenPrefab != null)
        {
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
        }

        // Фиксированное время задержки 3 секунды
        float transitionTime = 3f;
        yield return new WaitForSeconds(transitionTime);

        // Сохраняем игрока между сценами
        DontDestroyOnLoad(player.gameObject);

        // Загружаем новую сцену
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, player);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneName);
        loadOperation.allowSceneActivation = false;

        // Ждём, пока сцена загрузится
        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        // Дополнительная секунда для экрана загрузки
        yield return new WaitForSeconds(1f);

        // Удаляем экран загрузки
        if (loadingScreenInstance != null)
        {
            Destroy(loadingScreenInstance);
        }

        // Разблокируем ввод
        isInputBlocked = false;
    }

    // Метод для выполнения действий после загрузки сцены
    private void OnSceneLoaded(Scene scene, Transform player)
    {
        // Находим точку появления (SpawnPoint) в целевой сцене
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            player.position = spawnPoint.transform.position;
            Debug.Log($"Player teleported to SpawnPoint at {spawnPoint.transform.position}");
        }
        else
        {
            Debug.LogWarning("No SpawnPoint found in the target scene!");
        }

        // Убираем подписку на событие после завершения
        SceneManager.sceneLoaded -= (sceneLoaded, mode) => OnSceneLoaded(sceneLoaded, player);
    }

    // Вспомогательный метод для поиска корневого объекта с указанным тегом
    private Transform GetRootObjectWithTag(GameObject obj, string tag)
    {
        // Проверяем сам объект
        if (obj.CompareTag(tag)) return obj.transform;

        // Проверяем родительский объект
        if (obj.transform.parent != null && obj.transform.parent.CompareTag(tag)) return obj.transform.parent;

        return null; // Если тег не найден
    }
}