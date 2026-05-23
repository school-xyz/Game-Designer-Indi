using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Массив из ссылок на компоненты UIScore")]
    public UIScore[] uiScore;
    [Tooltip("Ссылка на компонент UIGameOver")]
    public UIGameOver uiGameOver;
    [Tooltip("Ссылка на HUD панель")]
    public GameObject hudPanel;

    [Tooltip("Базовое значение здоровья игрока")]
    public int healthAmount = 10;

    public int coins = 0;
    private GameSpeed _gameSpeed;

    private void Start()
    {
        //Размараживаем время
        Time.timeScale = 1;

        //Обнуляем количество монет
        coins = 0;

        //Проверяем есть ли компонент GameSpeed если нет то ищем его среди компонентов на текущем объекте
        if (_gameSpeed == null)
            _gameSpeed = GetComponent<GameSpeed>();

        //Вызываем метод увеличения количества монет, но увеличиваем на 0 чтобы просто обновить все текстовые компоненты в начале
        AddCoins(0);
    }

    //Тут идет увеличение количества монет, вызывов отображения в UI, и увеличение скорости в случае достижения условии
    public void AddCoins(int amount)
    {
        //Увеличиваем текущее количество монет
        coins += amount;
        //Проходимся по массиву UIScore и вызываем метод обновления текста
        foreach (var item in uiScore)
        {
            //Вызываем метод обновления текста и передаем нужно значение, переводим монеты в текстовый вид
            item.UpdateScoreLabel(coins.ToString());
        }

        //Проверяем если текущее количество монет кратно нужному значение и оно не равно нулю тогда увеличиваем скорость
        
            _gameSpeed.IncreaseSpeed();
    }

    //Наносим урон по игроку и когда здоровье на нуле вызывать GameOver
    public void DealDamage(int damage) 
    {
        //Уменьшаем количество здоровья за счет урона
        healthAmount -= damage;
        //Проверяем что здоровье ниже или равно нулю
        if (healthAmount <= 0)
        {
            //В случае положительного ответа от условия вызываем GameOver у компонента UIGameOver
            uiGameOver.OnGameOver();
            //Убираем игровой HUD 
            hudPanel.SetActive(false);
        }
    }

    //Получаем текущую базовую скорость
    public float GetSpeed()
    { 
        return _gameSpeed.baseSpeed;
    }
}
