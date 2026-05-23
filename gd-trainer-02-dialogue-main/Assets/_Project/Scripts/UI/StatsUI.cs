using System;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{ 
    [Tooltip("Label для отображения Монет")]
    [SerializeField]
    private TMP_Text _coinsText;
    [Tooltip("Label для отображения Уровня Репутации")]
    [SerializeField] 
    private TMP_Text _reputationText;

    public static Action<int> onChangeCoins;
    public static Action<float> onChangeReputation;
    
    // Вызов: подписывается на события изменения статистики
    private void Awake()
    {
       onChangeCoins += OnChangeCoins;
       onChangeReputation += OnChangeReputation;
       OnChangeCoins(0);
       OnChangeReputation(0);
    }
    // Вызов: отписывается от событий изменения статистики
    private void OnDestroy()
    {
        onChangeCoins -= OnChangeCoins; 
        onChangeReputation -= OnChangeReputation;
    }
    // Вызов: обновляет текст количества монет
    private void OnChangeCoins(int value)
    {
        _coinsText.text = "COINS: X" + value.ToString();
    }
    // Вызов: обновляет текст репутации
    private void OnChangeReputation(float value)
    {
        _reputationText.text = "REPUTATION: X"+ value.ToString("0.0");
    }
}
