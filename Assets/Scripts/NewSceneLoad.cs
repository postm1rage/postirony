using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Имя сцены, которую нужно загрузить
    [SerializeField] private GameObject loadingScreenPrefab; // Префаб загрузочного экрана
    [SerializeField] private float loadingScreenDuration = 3f; // Длительность загрузочного экрана в секундах

    private GameObject loadingScreenInstance; // Экземпляр загрузочного экрана

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у объекта тег "Player"
        if (collision.CompareTag("Player"))
        {
            // Ищем объект с тегом "Boss" на сцене
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");

            if (boss == null) // Если объекта с тегом "Boss" нет
            {
                // Ищем объект с тегом "BackgroundMusic" и сохраняем его между сценами
                GameObject backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
                if (backgroundMusic != null)
                {
                    DontDestroyOnLoad(backgroundMusic);
                }

                // Начинаем загрузку новой сцены
                StartCoroutine(LoadSceneWithLoadingScreen(sceneToLoad));
            }
            else
            {
                Debug.Log("Сцена не загружена, так как на сцене есть объект с тегом 'Boss'.");
            }
        }
    }

    private System.Collections.IEnumerator LoadSceneWithLoadingScreen(string sceneName)
    {
        // Создаем загрузочный экран
        if (loadingScreenPrefab != null)
        {
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
        }

        // Начинаем асинхронную загрузку сцены
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false; // Останавливаем автоматическую активацию сцены

        // Ждем завершения загрузки сцены
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                break; // Сцена загружена, но не активирована
            }
            yield return null;
        }

        // Ждем 3 секунды для отображения загрузочного экрана
        yield return new WaitForSeconds(loadingScreenDuration);

        // Активируем загруженную сцену
        asyncOperation.allowSceneActivation = true;

        // Удаляем загрузочный экран после завершения загрузки
        if (loadingScreenInstance != null)
        {
            Destroy(loadingScreenInstance);
        }
    }
}