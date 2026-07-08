using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [Tooltip("Данные ресурса для отображения (Gold, Gems, Energy)")]
    [SerializeField] private CurrencyData _currency;

    [Tooltip("Система уровней — только для полоски опыта (Exp / Lvl)")]
    [SerializeField] private LevelSystem _levelSystem;

    [Tooltip("Текстовые поля количества ресурса")]
    [SerializeField] private Text[] _amountTexts;

    [Tooltip("Заполнение полоски (Image Type = Filled)")]
    [SerializeField] private Image _fillImage;

    [Tooltip("Префикс перед числом (например, ENERGY или x)")]
    [SerializeField] private string _prefix = "x";

    private void Awake()
    {
        if (_currency != null)
            _currency.OnAmountChanged += UpdateAmount;

        if (_levelSystem != null)
            _levelSystem.OnLevelChanged += UpdateXP;
    }

    private void OnDestroy()
    {
        if (_currency != null)
            _currency.OnAmountChanged -= UpdateAmount;

        if (_levelSystem != null)
            _levelSystem.OnLevelChanged -= UpdateXP;
    }

    private void Start()
    {
        if (_currency != null)
            UpdateAmount(_currency.currentAmount);

        if (_levelSystem != null && _currency == null)
            UpdateXP(_levelSystem.currentLevel);
    }

    //Вызов: Обновляет текст и полоску ресурса
    private void UpdateAmount(int value)
    {
        foreach (Text text in _amountTexts)
        {
            if (text != null)
            {
                if (_fillImage != null && _currency != null && _currency.maxAmount > 0)
                    text.text = string.IsNullOrEmpty(_prefix) ? $"{value}/{_currency.maxAmount}" : $"{_prefix} {value}/{_currency.maxAmount}";
                else
                    text.text = _prefix + value;
            }
        }

        if (_fillImage != null && _currency != null && _currency.maxAmount > 0)
            _fillImage.fillAmount = (float)value / _currency.maxAmount;
    }

    //Вызов: Обновляет полоску опыта (только если нет ресурса)
    private void UpdateXP(int level)
    {
        if (_currency != null || _levelSystem == null)
            return;

        if (_fillImage != null)
            _fillImage.fillAmount = _levelSystem.GetXpFillAmount();
    }
}
