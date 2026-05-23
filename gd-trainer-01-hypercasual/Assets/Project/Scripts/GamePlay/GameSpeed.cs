using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    [Tooltip("Ссылка на GameManager")]
    public GameManager gameManager;
    [Tooltip("Базовое значение времени")]
    public float baseSpeed = 5f;
    [Tooltip("Инкремент базового значения времени")]
    public float speedIncrement = 5;
    [Tooltip("Порог для увеличения базовой скорости")]
    public int scoreThreshold = 100; // ← ЗАДАНИЕ

    public void IncreaseSpeed()
    {
        if (gameManager.coins % scoreThreshold == 0 && gameManager.coins != 0) // ← ЗАДАНИЕ
            baseSpeed += speedIncrement;
    }
}
