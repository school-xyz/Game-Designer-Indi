using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Количество собранных монет за попытку
    public int CollectedCoins { get; private set; }
    
    // Флаг завершения уровня
    public bool IsLevelComplete { get; private set; }

    private int _coinsOnLevel;

    public int CoinsOnLevel => _coinsOnLevel;

    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _gamePanel;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        _coinsOnLevel = FindObjectsOfType<Collectible>().Length;
    }

    // Вызов: увеличивает счётчик собранных монет
    public void AddCollectible(int coinValue)
    {
        CollectedCoins += coinValue;
    }

    // Вызов: отмечает уровень как пройденный
    public void CompleteLevel()
    {
        if (IsLevelComplete)
            return;

        IsLevelComplete = true;
        _gamePanel.SetActive(false);
        _finishPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickNext()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
