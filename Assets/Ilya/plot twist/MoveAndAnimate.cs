using UnityEngine;
using System.Collections;


public class MoveAndAnimate : MonoBehaviour
{
    public Animator animator; // Ссылка на компонент Animator
    public float moveDistance = 1000f; // Расстояние, на которое нужно переместить объект
    public float moveDuration = 1f; // Время перемещения
    private Vector3 initialPosition; // Начальная позиция объекта

    void Start()
    {
        initialPosition = transform.position; // Сохраняем начальную позицию
        StartCoroutine(MoveObject()); // Запускаем корутину для движения
    }

    private IEnumerator MoveObject()
    {
        animator.SetBool("IsMoving", true); // Запускаем анимацию (например, "IsMoving" - параметр в Animator)

        Vector3 targetPosition = initialPosition + new Vector3(moveDistance, 0, 0); // Вычисляем целевую позицию
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждем до следующего кадра
        }

        transform.position = targetPosition; // Устанавливаем окончательную позицию
        animator.SetBool("IsMoving", false); // Останавливаем анимацию
    }
}
