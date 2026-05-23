using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Tooltip("Базовое значение монеты")]
    public int coinValue = 10;
    [Tooltip("Множитель базового значения монет")]
    public int scoreMultiplier = 1; // ← ЗАДАНИЕ
    [Tooltip("Звук подбора монет")]
    public AudioClip collectSound = null;
    [Tooltip("Ссылка на GameManager")]
    public GameManager gameManager; 

    private void Update()
    {
        //Двигаем объект в лево с базовой скоростью
        transform.position += (new Vector3(-1, 0, 0) * gameManager.GetSpeed()) * Time.deltaTime;
    }

    //Подбираем монету и начисляет очки игроку
    public void PickUp()
    {
        //AudioSource.PlayClipAtPoint(collectSound, transform.position); // ← ЗАДАНИЕ: раскомментируйте

        //Вычесляем итоговое значение монеты
        int totalValue = coinValue * scoreMultiplier;
        //Добавляем итоговое значение к общему значению
        gameManager.AddCoins(totalValue);

        //Уничтожаем монету
        Destroy(gameObject);
    }
}
