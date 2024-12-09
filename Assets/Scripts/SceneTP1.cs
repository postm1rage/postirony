using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportWithLoadingScreen : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "FirstRoom"; // ��� ������� �����
    [SerializeField] private GameObject loadingScreenPrefab; // ������ ������� ������������ ������
    private GameObject loadingScreenInstance; // ��������� ������������ ������
    private bool isInputBlocked = false; // ���� ���������� �����

    private void Update()
    {
        // ��������� ��� ��������, ���� ������� ����� ��������
        if (isInputBlocked)
        {
            // ��������� ����� ���������� ����� (��������, ����� CursorLock ��� � UI)
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ������ �� ������ � ����� "Player" � �������
        Transform rootObject = GetRootObjectWithTag(other.gameObject, "Player");
        if (rootObject != null)
        {
            Debug.Log($"Player detected! Preparing to teleport to scene {targetSceneName}");

            // ��������� �������� ��� ����������� ������ �������� � ������������
            StartCoroutine(ShowLoadingScreenAndTeleport(rootObject));
        }
    }

    // ������� ��� ����������� ������ �������� � ������������
    private System.Collections.IEnumerator ShowLoadingScreenAndTeleport(Transform player)
    {
        // ���������� ���������� �����
        isInputBlocked = true;

        // ������ ����� ��������
        if (loadingScreenPrefab != null)
        {
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
        }

        // ������������� ����� �������� 3 �������
        float transitionTime = 3f;
        yield return new WaitForSeconds(transitionTime);

        // ��������� ������ ����� �������
        DontDestroyOnLoad(player.gameObject);

        // ��������� ����� �����
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, player);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneName);
        loadOperation.allowSceneActivation = false;

        // ���, ���� ����� ����������
        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        // �������������� ������� ��� ������ ��������
        yield return new WaitForSeconds(1f);

        // ������� ����� ��������
        if (loadingScreenInstance != null)
        {
            Destroy(loadingScreenInstance);
        }

        // ������������ ����
        isInputBlocked = false;
    }

    // ����� ��� ���������� �������� ����� �������� �����
    private void OnSceneLoaded(Scene scene, Transform player)
    {
        // ������� ����� ��������� (SpawnPoint) � ������� �����
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

        // ������� �������� �� ������� ����� ����������
        SceneManager.sceneLoaded -= (sceneLoaded, mode) => OnSceneLoaded(sceneLoaded, player);
    }

    // ��������������� ����� ��� ������ ��������� ������� � ��������� �����
    private Transform GetRootObjectWithTag(GameObject obj, string tag)
    {
        // ��������� ��� ������
        if (obj.CompareTag(tag)) return obj.transform;

        // ��������� ������������ ������
        if (obj.transform.parent != null && obj.transform.parent.CompareTag(tag)) return obj.transform.parent;

        return null; // ���� ��� �� ������
    }
}