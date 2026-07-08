using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Tooltip("Версия формата сохранения")]
    public int saveVersion = 1;
    [Tooltip("Время последнего сохранения")]
    public string lastSaveTime;

    [Tooltip("Количество золота")]
    public int gold;
    [Tooltip("Количество гемов")]
    public int gems;

    // TODO (Тренажёр 5.5): сохранять и загружать энергию
    [Tooltip("Количество энергии")]
    public int energy;

    [Tooltip("Сколько раз игрок кликнул")]
    public int totalClicks;
    [Tooltip("Текущий уровень игрока")]
    public int currentLevel = 1;
    [Tooltip("Текущий опыт игрока")]
    public int currentXP;

    // TODO (Тренажёр 5.5): сохранять и загружать купленные улучшения (JsonUtility не пишет List — используйте string[])
    [Tooltip("Id купленных улучшений")]
    public string[] purchasedUpgrades;
}
