using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // ���������, ���������� �� ��� ��������� MusicManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // ���������� ����� ������, ���� ��� ���� ������������
            return;
        }

        // ������������� ������� ������ ��� ������������ ���������
        instance = this;
        DontDestroyOnLoad(gameObject); // �� ���������� ������ ��� ����� �����
    }
}