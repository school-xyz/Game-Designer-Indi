using System;

//Объяснение: централизованные игровые события экономики
public static class GameEvents
{
    //Объяснение: событие клика по основному объекту
    public static Action OnCookieClicked;

    //Объяснение: событие покупки улучшения
    public static Action<UpgradeData> OnUpgradePurchased;
}
