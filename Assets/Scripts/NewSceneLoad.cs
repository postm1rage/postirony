using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // ��� �����, ������� ����� ���������
    [SerializeField] private GameObject loadingScreenPrefab; // ������ ������������ ������
    [SerializeField] private float loadingScreenDuration = 3f; // ������������ ������������ ������ � ��������

    private GameObject loadingScreenInstance; // ��������� ������������ ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���� �� � ������� ��� "Player"
        if (collision.CompareTag("Player"))
        {
            // ���� ������ � ����� "Boss" �� �����
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");

            if (boss == null) // ���� ������� � ����� "Boss" ���
            {
                // ���� ������ � ����� "BackgroundMusic" � ��������� ��� ����� �������
                GameObject backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
                if (backgroundMusic != null)
                {
                    DontDestroyOnLoad(backgroundMusic);
                }

                // �������� �������� ����� �����
                StartCoroutine(LoadSceneWithLoadingScreen(sceneToLoad));
            }
            else
            {
                Debug.Log("����� �� ���������, ��� ��� �� ����� ���� ������ � ����� 'Boss'.");
            }
        }
    }

    private System.Collections.IEnumerator LoadSceneWithLoadingScreen(string sceneName)
    {
        // ������� ����������� �����
        if (loadingScreenPrefab != null)
        {
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
        }

        // �������� ����������� �������� �����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false; // ������������� �������������� ��������� �����

        // ���� ���������� �������� �����
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                break; // ����� ���������, �� �� ������������
            }
            yield return null;
        }

        // ���� 3 ������� ��� ����������� ������������ ������
        yield return new WaitForSeconds(loadingScreenDuration);

        // ���������� ����������� �����
        asyncOperation.allowSceneActivation = true;

        // ������� ����������� ����� ����� ���������� ��������
        if (loadingScreenInstance != null)
        {
            Destroy(loadingScreenInstance);
        }
    }
}