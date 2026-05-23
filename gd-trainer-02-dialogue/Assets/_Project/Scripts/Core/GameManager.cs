using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Ссылка на компонент PlayerState")]
    [SerializeField] private PlayerState _playerState;
    [Tooltip("Ссылка на компонент UI Manager")]
    [SerializeField] private UIManager _uiManager;
    
    //Вызов: Возвращает ссылку на компонент PlayerState
    public PlayerState GetPlayerState() =>  _playerState;
    //Вызов: Возвращает ссылку на компонент UIManager
    public UIManager GetUIManager() =>  _uiManager;
    //Вызов: Игровое время становится 0 в случае если оно 1 в другом случае 1
    public void OnClickPause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
