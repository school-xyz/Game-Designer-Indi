using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Ссылка на систему ресурсов")]
    [SerializeField] private ResourceSystem _resourceSystem;
    [Tooltip("Ссылка на загрузчик конфигов")]
    [SerializeField] private ConfigLoader _configLoader;
    [Tooltip("Ссылка на систему уровней")]
    [SerializeField] private LevelSystem _levelSystem;
    [Tooltip("Ссылка на UI менеджер")]
    [SerializeField] private UIManager _uiManager;
    [Tooltip("Ссылка на магазин улучшений")]
    [SerializeField] private ShopManager _shopManager;
    [Tooltip("Ресурс золота")]
    [SerializeField] private CurrencyData _gold;
    [Tooltip("Улучшение силы клика")]
    [SerializeField] private UpgradeData _clickUpgrade;
    [Tooltip("Улучшение фабрики (пассивный доход)")]
    [SerializeField] private UpgradeData _factoryUpgrade;
    [Tooltip("Все улучшения для сохранения и загрузки")]
    [SerializeField] private UpgradeData[] _upgrades;

    private int _clickReward;
    private int _passiveAmount;
    private float _passiveTimer;
    private bool _passiveActive;

    //Объяснение: сколько раз игрок кликнул за сессию
    public int TotalClicks { get; private set; }

    private void Awake()
    {
        GameEvents.OnCookieClicked += OnCookieClick;
    }

    private void OnDestroy()
    {
        GameEvents.OnCookieClicked -= OnCookieClick;
    }

    //Вызов: Стартовое состояние новой игры (вызывается из SaveManager)
    public void InitializeNewGame()
    {
        ApplyBalance();
        ResetPassiveIncome();
        ResetUpgrades();
        RefreshAllUI();
    }

    private void Update()
    {
        if (!_passiveActive)
            return;

        _passiveTimer += Time.deltaTime;
        if (_passiveTimer < _configLoader.Balance.passiveIncomeInterval)
            return;

        _passiveTimer = 0f;
        _resourceSystem.AddResource(_gold, _passiveAmount, "passive_income");
    }

    //Вызов: Обрабатывает клик по печеньке
    private void OnCookieClick()
    {
        // ← ЗАДАНИЕ (Тренажёр 5.2/5.3): расход энергии при клике через ResourceSystem (clickEnergyCost из balance.json)

        _resourceSystem.AddResource(_gold, _clickReward, "click_reward");
        TotalClicks++;
        _levelSystem.AddXP(1);
        _uiManager.RefreshClickReward(_clickReward);
    }

    //Вызов: Покупает улучшение из магазина
    public bool TryPurchaseUpgrade(UpgradeData upgrade)
    {
        if (upgrade == null || upgrade.isPurchased)
            return false;

        if (!_resourceSystem.SpendResource(upgrade.costCurrency, upgrade.cost, "upgrade_purchase"))
            return false;

        upgrade.isPurchased = true;
        if (upgrade.effectType == UpgradeEffectType.ClickPower)
            _clickReward += upgrade.effectValue;
        else
        {
            _passiveActive = true;
            _passiveAmount += upgrade.effectValue;
        }

        GameEvents.OnUpgradePurchased?.Invoke(upgrade);
        RefreshAllUI();
        return true;
    }

    //Вызов: Сбрасывает игру к начальному состоянию
    public void ResetGame()
    {
        TotalClicks = 0;
        _passiveTimer = 0f;
        ApplyBalance();
        ResetPassiveIncome();
        _resourceSystem.InitializeCurrencies();
        ResetUpgrades();
        _levelSystem.ApplySave(1, 0);
        RefreshAllUI();
    }

    //Вызов: Восстанавливает прогресс после загрузки
    public void ApplySave(SaveData data)
    {
        TotalClicks = data.totalClicks;
        ResetUpgrades();
        ApplyBalance();
        ResetPassiveIncome();

        if (data.purchasedUpgrades != null)
        {
            // ← ЗАДАНИЕ (Тренажёр 5.5): восстановить купленные улучшения из data.purchasedUpgrades
        }

        RefreshAllUI();
    }

    //Вызов: Возвращает id купленных улучшений для сохранения
    public List<string> GetPurchasedUpgradeIds()
    {
        List<string> ids = new List<string>();
        foreach (UpgradeData upgrade in _upgrades)
        {
            if (upgrade != null && upgrade.isPurchased)
                ids.Add(upgrade.upgradeId);
        }
        return ids;
    }

    //Вызов: Возвращает текущую награду за клик
    public int GetClickReward() => _clickReward;

    //Вызов: Возвращает текущий пассивный доход в секунду
    public int GetPassiveIncome() => _passiveAmount;

    //Вызов: Применяет значения из balance.json
    private void ApplyBalance()
    {
        BalanceConfig b = _configLoader.Balance;
        _clickReward = b.clickReward;

        if (_clickUpgrade != null)
        {
            _clickUpgrade.cost = b.clickUpgradeCost;
            _clickUpgrade.effectValue = b.clickUpgradeBonus;
        }

        if (_factoryUpgrade != null)
        {
            _factoryUpgrade.cost = b.factoryUpgradeCost;
            _factoryUpgrade.effectValue = b.factoryPassiveBonus;
        }
    }

    //Вызов: Сбрасывает пассивный доход до нуля
    private void ResetPassiveIncome()
    {
        _passiveAmount = 0;
        _passiveActive = false;
        _passiveTimer = 0f;
    }

    //Вызов: Обновляет все надписи HUD после загрузки или сброса
    private void RefreshAllUI()
    {
        _uiManager.RefreshAll(this);
        _resourceSystem.NotifyAllCurrenciesChanged();

        if (_shopManager != null)
            _shopManager.RefreshItems();

        if (_levelSystem != null)
            _levelSystem.NotifyLevelChanged();
    }

    //Вызов: Сбрасывает флаги покупки всех улучшений
    private void ResetUpgrades()
    {
        foreach (UpgradeData upgrade in _upgrades)
        {
            if (upgrade != null)
                upgrade.isPurchased = false;
        }
    }

    //Вызов: Ищет улучшение по id
    private UpgradeData FindUpgrade(string upgradeId)
    {
        foreach (UpgradeData upgrade in _upgrades)
        {
            if (upgrade != null && upgrade.upgradeId == upgradeId)
                return upgrade;
        }
        return null;
    }
}
