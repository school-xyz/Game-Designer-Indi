using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "Economy/Currency")]
public class CurrencyData : ScriptableObject
{
    [Tooltip("Название ресурса (Gold, Gems, Energy)")]
    public string currencyName;

    [Tooltip("Иконка ресурса для UI")]
    public Sprite icon;

    [Tooltip("Стартовое количество при новой игре")]
    public int startAmount;

    [Tooltip("Максимальное количество ресурса")]
    public int maxAmount;

    [Tooltip("Текущее количество (runtime)")]
    public int currentAmount;

    //Объяснение: событие для обновления UI при изменении количества
    public event Action<int> OnAmountChanged;

    //Вызов: Сбрасывает количество к стартовому значению
    public void ResetToStart()
    {
        SetAmount(startAmount, false);
    }

    //Вызов: Устанавливает количество с учётом лимитов и уведомляет UI
    public void SetAmount(int value, bool invokeEvent = true)
    {
        currentAmount = Mathf.Clamp(value, 0, maxAmount);

        if (invokeEvent)
            OnAmountChanged?.Invoke(currentAmount);
    }

    //Вызов: Принудительно обновляет UI текущим значением
    public void NotifyAmountChanged()
    {
        OnAmountChanged?.Invoke(currentAmount);
    }
}
