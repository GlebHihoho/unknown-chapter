using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f; // Задайте желаемое расстояние для взаимодействия.

    private void Update()
    {
        // Получите текущую позицию персонажа и объекта для взаимодействия.
        Vector3 characterPosition = transform.position;
        Vector3 interactableObjectPosition = GetInteractableObjectPosition(); // Реализуйте этот метод для получения позиции объекта для взаимодействия.

        // Вычислите расстояние между ними.
        float distance = Vector3.Distance(characterPosition, interactableObjectPosition);

        // Если расстояние меньше заданного, выполните взаимодействие.
        if (distance <= interactionDistance)
        {
            PerformInteraction(); // Реализуйте этот метод для выполнения взаимодействия.
        }
    }

    private Vector3 GetInteractableObjectPosition()
    {
        // Реализуйте этот метод для получения позиции объекта для взаимодействия.
        // Например, вы можете использовать Raycast или другие методы, чтобы найти объект в определенном направлении от персонажа.
        // Возвращайте позицию найденного объекта.
        return Vector3.zero;
    }

    private void PerformInteraction()
    {
        // Реализуйте этот метод для выполнения взаимодействия с объектом.
        // Например, вы можете активировать объект, показать диалоговое окно и т. д.
    }
}