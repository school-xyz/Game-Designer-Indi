using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Объяснение: вкладка магазина (клик или фабрика)
public enum UpgradeType
{
    Click,
    Factory
}

public class ShopManager : MonoBehaviour
{
    [Tooltip("Список улучшений силы клика")]
    [SerializeField] private UpgradeData[] _clickList;
    [Tooltip("Список улучшений пассивного дохода")]
    [SerializeField] private UpgradeData[] _factoryList;

    [Tooltip("Текст заголовка вкладки магазина (CLICKS / FACTORIES)")]
    [SerializeField] private Text _shopLabel;

    [Tooltip("Префаб карточки товара в магазине")]
    [SerializeField] private ShopItemUI _pref;
    [Tooltip("Контейнер для списка товаров")]
    [SerializeField] private Transform _content;

    [Tooltip("Ссылка на GameManager")]
    [SerializeField] private GameManager _gameManager;

    private List<ShopItemUI> _filledData = new List<ShopItemUI>();

    private UpgradeType _currentType;

    private void Awake()
    {
        _currentType = UpgradeType.Click;
        FillShop(_currentType);
    }

    //Вызов: Заполняет магазин товарами выбранного типа
    private void FillShop(UpgradeType type)
    {
        if (_filledData.Count > 0)
        {
            foreach (ShopItemUI item in _filledData)
                Destroy(item.gameObject);
        }

        _filledData.Clear();

        UpgradeData[] data = type == UpgradeType.Click ? _clickList : _factoryList;
        foreach (UpgradeData item in data)
        {
            ShopItemUI newItem = Instantiate(_pref, _content);
            newItem.Init(item, _gameManager);
            _filledData.Add(newItem);
        }
    }

    //Вызов: Обновляет карточки магазина после загрузки сейва
    public void RefreshItems()
    {
        foreach (ShopItemUI item in _filledData)
            item.RefreshView();
    }

    //Вызов: Переключает список между кликовыми и фабричными улучшениями
    public void ChangeShopList()
    {
        _currentType = _currentType == UpgradeType.Click ? UpgradeType.Factory : UpgradeType.Click;
        _shopLabel.text = _currentType == UpgradeType.Click ? "CLICKS" : "FACTORIES";
        FillShop(_currentType);
    }
}
