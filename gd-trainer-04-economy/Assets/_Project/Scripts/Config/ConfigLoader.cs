using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    //Объяснение: загруженные параметры balance.json
    public BalanceConfig Balance { get; private set; } = new BalanceConfig();
    //Объяснение: загруженные строки levels.csv
    public List<LevelConfigRow> Levels { get; private set; } = new List<LevelConfigRow>();

    private void Awake()
    {
        LoadBalance();
        LoadLevelsCsv();
    }

    //Вызов: Загружает balance.json из StreamingAssets
    public void LoadBalance()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "balance.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("balance.json не найден в StreamingAssets.");
            return;
        }

        Balance = JsonUtility.FromJson<BalanceConfig>(File.ReadAllText(path)) ?? new BalanceConfig();
    }

    //Вызов: Загружает levels.csv из StreamingAssets
    public void LoadLevelsCsv()
    {
        Levels.Clear();
        string path = Path.Combine(Application.streamingAssetsPath, "levels.csv");
        if (!File.Exists(path))
        {
            Debug.LogWarning("levels.csv не найден в StreamingAssets.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] p = lines[i].Split(',');
            if (p.Length < 3)
                continue;

            Levels.Add(new LevelConfigRow
            {
                level = int.Parse(p[0].Trim()),
                requiredXP = int.Parse(p[1].Trim()),
                reward = int.Parse(p[2].Trim())
            });
        }
    }
}

[Serializable]
public class BalanceConfig
{
    [Tooltip("Награда за клик")]
    public int clickReward = 10;
    [Tooltip("Расход энергии за клик")]
    public int clickEnergyCost = 5;
    [Tooltip("Пассивный доход за тик")]
    public int passiveIncomeAmount = 1;
    [Tooltip("Интервал пассивного дохода в секундах")]
    public float passiveIncomeInterval = 1f;
    [Tooltip("Стоимость улучшения клика")]
    public int clickUpgradeCost = 100;
    [Tooltip("Бонус улучшения клика")]
    public int clickUpgradeBonus = 5;
    [Tooltip("Стоимость улучшения фабрики")]
    public int factoryUpgradeCost = 250;
    [Tooltip("Бонус пассивного дохода от фабрики")]
    public int factoryPassiveBonus = 1;
}

[Serializable]
public class LevelConfigRow
{
    [Tooltip("Номер уровня")]
    public int level;
    [Tooltip("Сколько опыта нужно для уровня")]
    public int requiredXP;
    [Tooltip("Награда за достижение уровня")]
    public int reward;
}
