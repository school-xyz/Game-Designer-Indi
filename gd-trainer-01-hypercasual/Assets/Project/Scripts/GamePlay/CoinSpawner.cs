using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameManager gameManager;

    [Tooltip("Список точек спавна")]
    public Transform[] spawnPositions;
    [Tooltip("Префаб монеты")]
    public CoinPickup coinPrefab;
    [Tooltip("Время через которое должны спавниться монеты")]
    public float spawnInterval;

    private float _time = 0f;

    public void Update()
    {
        _time += Time.deltaTime; // Увеличиваем текущее время на Время между кадрами
        if (_time >= spawnInterval) // Проверяем что текущее время больше спавн интервала и вызываем метод Спавна преград
        {
            SpawnCoin();
            _time = 0;
        }
    }

    private void SpawnCoin()
    {
        // Спавним через Instantiate префаб преграды, позицию задаем рандомную из массива позиции
        CoinPickup newCoin = Instantiate(coinPrefab, spawnPositions[Random.Range(0,spawnPositions.Length)].position, Quaternion.identity);
        newCoin.gameManager = gameManager; // Передаем ссылку на GameManager чтобы в дальнейшем преграда могла обращаться к нему
    }
}
