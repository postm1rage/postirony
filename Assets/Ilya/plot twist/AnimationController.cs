using UnityEngine;
using UnityEngine.SceneManagement; // Для смены сцены

public class AnimationController : MonoBehaviour 
{ 
    private Animator animator; // Компонент Animator
    private int currentAnimationIndex = 0; // Индекс текущей анимации

    void Start() 
    { 
        animator = GetComponent<Animator>(); // Получаем компонент Animator
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
        // Здесь вы можете установить условие для проверки количества анимаций
        if (currentAnimationIndex >= animator.runtimeAnimatorController.animationClips.Length) 
        { 
            currentAnimationIndex = 0; // Сброс индекса, если достигли конца 
        }
        PlayAnimation(currentAnimationIndex); // Запускаем следующую анимацию
    } 

    void PlayAnimation(int index)
    {
        // Здесь предполагается, что у вас есть параметры в Animator, которые соответствуют индексам анимаций.
        // Например, если у вас есть параметр "AnimationIndex" типа Integer.
        animator.SetInteger("AnimationIndex", index);
    }

    void SkipScene() 
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Переход к следующей сцене 
    } 
}
