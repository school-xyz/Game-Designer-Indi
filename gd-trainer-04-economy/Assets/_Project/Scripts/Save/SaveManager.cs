using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Tooltip("Ссылка на систему ресурсов")]
    [SerializeField] private ResourceSystem _resourceSystem;

    [Tooltip("Ссылка на GameManager")]
    [SerializeField] private GameManager _gameManager;

    [Tooltip("Ссылка на LevelSystem")]
    [SerializeField] private LevelSystem _levelSystem;

    [Tooltip("Интервал автосохранения в секундах (Тренажёр 5.5)")]
    [SerializeField] private float _autoSaveInterval = 30f;

    //Объяснение: путь к файлу save.json в persistentDataPath
    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Start()
    {
        if (File.Exists(SavePath))
            Load();
        else
        {
            _resourceSystem.InitializeCurrencies();
            _gameManager.InitializeNewGame();
        }
    }

    // ← ЗАДАНИЕ (Тренажёр 5.5): автосохранение раз в _autoSaveInterval секунд

    //Вызов: Сохраняет экономическое состояние в файл
    public void Save()
    {
        SaveData data = new SaveData();

        data.saveVersion = 1;
        data.lastSaveTime = DateTime.Now.ToString("o");
        data.gold = GetAmount("Gold");
        data.gems = GetAmount("Gems");
        // ← ЗАДАНИЕ (Тренажёр 5.5): data.energy = GetAmount("Energy");
        data.totalClicks = _gameManager.TotalClicks;
        data.currentLevel = _levelSystem.currentLevel;
        data.currentXP = _levelSystem.currentXP;
        // ← ЗАДАНИЕ (Тренажёр 5.5): data.purchasedUpgrades = _gameManager.GetPurchasedUpgradeIds().ToArray();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Game saved");
    }

    //Вызов: Загружает экономическое состояние из файла
    public void Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("Файл сохранения не найден.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        data = MigrateSave(data);

        _resourceSystem.ApplySavedAmounts(data.gold, data.gems);
        // ← ЗАДАНИЕ (Тренажёр 5.5): восстановить energy из data.energy
        _levelSystem.ApplySave(data.currentLevel, data.currentXP);
        _gameManager.ApplySave(data);

        Debug.Log("Game loaded");
    }

    //Вызов: Поднимает старый сейв до актуальной версии
    private SaveData MigrateSave(SaveData data)
    {
        // ← ЗАДАНИЕ (Тренажёр 5.5): миграция saveVersion 1 → 2 (например, дефолтная energy)
        return data;
    }

    //Вызов: Удаляет сохранение и сбрасывает игру
    public void Delete()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);

        _gameManager.ResetGame();
    }

    //Вызов: Возвращает количество ресурса по имени
    private int GetAmount(string currencyName)
    {
        CurrencyData currency = _resourceSystem.GetCurrency(currencyName);
        return currency != null ? currency.currentAmount : 0;
    }
}
