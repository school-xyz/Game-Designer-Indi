using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [Tooltip("Ссылка на Панель GameOver")]
    public GameObject gameOverPanel = null;

    //Замараживаем время и показываем панель GameOver
    public void OnGameOver()
    {
        //Ставим значение времени на 0 тем самым замараживая его
        Time.timeScale = 0; 
        //Активируем панель GameOver
        gameOverPanel.SetActive(true);
    }

    public void OnClickRestart()
    {
        // По клику на кнопку перезапускаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
