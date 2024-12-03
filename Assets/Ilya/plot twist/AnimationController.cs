using UnityEngine;
using UnityEngine.SceneManagement; // Для смены сцены 

public class AnimationController : MonoBehaviour   
{   
    private Animator animator; // Компонент Animator  
    private AudioSource audioSource; // Компонент AudioSource 
    private int currentAnimationIndex = 0; // Индекс текущей анимации  
    private bool shouldStopSound = false; // Флаг для остановки звука

    public AudioClip[] animationSounds; // Массив аудиоклипов для каждой анимации 
    public float[] soundDurations; // Массив длительностей для каждого звука 

    public Vector3 lastAnimationOffset = new Vector3(0, 1, 0); // Смещение для последней анимации
    public Vector3 lastAnimationScale = new Vector3(2, 2, 2); // Масштаб для последней анимации

    void Start()   
    {   
        animator = GetComponent<Animator>(); // Получаем компонент Animator  
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource 
        PlayAnimation(currentAnimationIndex); // Запускаем первую анимацию  
    }   

    void Update()   
    {   
        if (Input.GetMouseButtonDown(0)) // Если нажата левая кнопка мыши   
        {   
            ShowNextAnimation();   
        }   

        if (Input.GetKeyDown(KeyCode.Space)) // Если нажата клавиша пробела   
        {   
            SkipScene();   
        }   
    }   

    void ShowNextAnimation()   
    {   
        currentAnimationIndex++; // Увеличиваем индекс текущей анимации  
        
        // Проверяем количество анимаций  
        if (currentAnimationIndex >= animator.runtimeAnimatorController.animationClips.Length)   
        {   
            // Если текущая анимация последняя, переходим к следующей сцене
            LoadNextScene();
            return; // Выходим из метода
        }  
        
        PlayAnimation(currentAnimationIndex); // Запускаем следующую анимацию  
    }   

    void PlayAnimation(int index)  
    {  
        animator.SetInteger("AnimationIndex", index);  

        if (shouldStopSound && audioSource.isPlaying)
        {
            audioSource.Stop(); // Остановим звук, если необходимо
            shouldStopSound = false; // Сбросим флаг
        }

        PlaySoundForAnimation(index);

        if (index == animator.runtimeAnimatorController.animationClips.Length - 1) 
        {
            transform.localPosition += lastAnimationOffset; 
            transform.localScale = lastAnimationScale; 
        }
        else
        {
            transform.localPosition = Vector3.zero; 
            transform.localScale = Vector3.one; 
        }
    }  

    void PlaySoundForAnimation(int index) 
    { 
        if (index >= 0 && index < animationSounds.Length && animationSounds[index] != null) 
        { 
            audioSource.clip = animationSounds[index]; 
            audioSource.Play(); 

            if (index < soundDurations.Length)
            {
                StartCoroutine(StopSoundAfterDuration(soundDurations[index]));
            }
        } 
    } 

    private System.Collections.IEnumerator StopSoundAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }
    }

    void LoadNextScene()   
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }   

    void SkipScene()   
    {        shouldStopSound = true; // Устанавливаем флаг для остановки звука
        LoadNextScene();   
    }   
}
