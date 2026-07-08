using UnityEngine;

//Объяснение: тип эффекта улучшения
public enum UpgradeEffectType
{
    ClickPower,
    PassiveIncome
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Economy/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Tooltip("Уникальный идентификатор улучшения для сохранений")]
    public string upgradeId;

    [Tooltip("Название улучшения в магазине")]
    public string displayName;

    [Tooltip("Описание улучшения в магазине")]
    public string displayDescription;

    [Tooltip("Ресурс, которым оплачивается улучшение")]
    public CurrencyData costCurrency;

    [Tooltip("Стоимость покупки")]
    public int cost;

    [Tooltip("Тип эффекта улучшения")]
    public UpgradeEffectType effectType;

    [Tooltip("Сила эффекта (бонус к клику или пассивный доход)")]
    public int effectValue;

    //Объяснение: куплено ли улучшение (runtime)
    [HideInInspector]
    public bool isPurchased;
}
