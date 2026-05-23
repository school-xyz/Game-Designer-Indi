using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("Ссылка на компонент GameManager")]
    public GameManager gameManager;
    [Tooltip("Список точек спавна")]
    public Transform[] spawnPositions;
    [Tooltip("Префаб преграды")]
    public ObstacleHit obstaclePrefab;
    [Tooltip("Время через которое должны спавниться преграды")]
    public float spawnInterval;

    private float _time = 0f; 

    public void Update()
    {
        // Увеличиваем время
        _time += Time.deltaTime;
        // Проверяем что текущее время больше спавн интервала и вызываем метод Спавна преград
        if (_time >= spawnInterval) 
        {
            SpawnObstacle();
            _time = 0;
        }
    }

    private void SpawnObstacle()
    {
        // Спавним через Instantiate префаб преграды, позицию задаем рандомную из массива позиции
        ObstacleHit newObstacle = Instantiate(obstaclePrefab, spawnPositions[Random.Range(0, spawnPositions.Length)].position,Quaternion.identity);
        // Передаем ссылку на GameManager чтобы в дальнейшем преграда могла обращаться к нему
        newObstacle.gameManager = gameManager; 
    }
}
