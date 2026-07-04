using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _collectedCoins;
    [SerializeField] private TMP_Text _healthTxt;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerController _player;

    private void LateUpdate()
    {
        _healthTxt.text = $"{_player._sessionHealth}/{_player._health}";

        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        foreach (var coin in _collectedCoins)
        {
            coin.text = $"{_gameManager.CollectedCoins}/{_gameManager.CoinsOnLevel}";
        }
    }
}
