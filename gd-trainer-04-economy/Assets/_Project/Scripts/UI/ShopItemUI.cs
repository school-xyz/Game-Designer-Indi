using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Tooltip("Текст названия улучшения")]
    [SerializeField] private Text _label;

    [Tooltip("Текст стоимости")]
    [SerializeField] private Text _priceText;

    [Tooltip("Текст описания улучшения")]
    [SerializeField] private Text _descriptionText;

    [Tooltip("Кнопка покупки")]
    [SerializeField] private Button _button;

    //Объяснение: данные улучшения (назначаются в Init)
    private UpgradeData _upgrade;

    //Объяснение: ссылка на GameManager (назначается в Init)
    private GameManager _gameManager;

    //Вызов: Назначает улучшение и подключает кнопку покупки
    public void Init(UpgradeData upgrade, GameManager gameManager)
    {
        _upgrade = upgrade;
        _gameManager = gameManager;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Buy);

        UpdateView();
    }

    //Вызов: Пытается купить улучшение
    private void Buy()
    {
        _gameManager.TryPurchaseUpgrade(_upgrade);
        UpdateView();
    }

    //Вызов: Обновляет тексты и состояние кнопки (после загрузки сейва)
    public void RefreshView()
    {
        if (_upgrade == null)
            return;

        UpdateView();
    }

    //Вызов: Обновляет тексты и состояние кнопки
    private void UpdateView()
    {
        _label.text = _upgrade.displayName;
        _descriptionText.text = _upgrade.displayDescription;

        if (_upgrade.isPurchased)
        {
            _priceText.text = "КУПЛЕНО";
            _button.interactable = false;
        }
        else
        {
            _priceText.text = _upgrade.cost.ToString();
            _button.interactable = true;
        }
    }
}
