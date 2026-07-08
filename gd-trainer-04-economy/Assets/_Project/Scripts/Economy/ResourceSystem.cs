using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    [Tooltip("Все ресурсы, участвующие в экономике сцены")]
    [SerializeField] private CurrencyData[] _currencies;

    [Tooltip("Ссылка на лог транзакций")]
    [SerializeField] private TransactionLog _transactionLog;

    //Вызов: Возвращает ресурс по имени
    public CurrencyData GetCurrency(string currencyName)
    {
        foreach (CurrencyData currency in _currencies)
        {
            if (currency != null && currency.currencyName == currencyName)
                return currency;
        }

        return null;
    }

    //Вызов: Добавляет ресурс и логирует операцию
    public bool AddResource(CurrencyData currency, int amount, string reason)
    {
        if (currency == null || amount <= 0)
            return false;

        currency.SetAmount(currency.currentAmount + amount);

        if (_transactionLog != null)
            _transactionLog.Log(currency.currencyName, amount, reason);

        return true;
    }

    //Вызов: Списывает ресурс с проверкой баланса
    public bool SpendResource(CurrencyData currency, int amount, string reason)
    {
        if (currency == null || amount <= 0)
            return false;

        if (currency.currentAmount < amount)
            return false;

        currency.SetAmount(currency.currentAmount - amount);

        if (_transactionLog != null)
            _transactionLog.Log(currency.currencyName, -amount, reason);

        return true;
    }

    //Вызов: Инициализирует стартовые значения всех ресурсов
    public void InitializeCurrencies()
    {
        foreach (CurrencyData currency in _currencies)
        {
            if (currency != null)
                currency.ResetToStart();
        }
    }

    //Вызов: Обновляет все HUD-счётчики ресурсов
    public void NotifyAllCurrenciesChanged()
    {
        foreach (CurrencyData currency in _currencies)
        {
            if (currency != null)
                currency.NotifyAmountChanged();
        }
    }

    //Вызов: Применяет сохранённые значения ресурсов
    public void ApplySavedAmounts(int gold, int gems)
    {
        CurrencyData goldCurrency = GetCurrency("Gold");
        CurrencyData gemsCurrency = GetCurrency("Gems");

        if (goldCurrency != null)
            goldCurrency.SetAmount(gold);

        if (gemsCurrency != null)
            gemsCurrency.SetAmount(gems);

        // ← ЗАДАНИЕ (Тренажёр 5.5): восстановить Energy из сейва
    }
}
