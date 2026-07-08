using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Tooltip("Ссылка на загрузчик конфигов")]
    [SerializeField] private ConfigLoader _configLoader;

    [Tooltip("Ресурс золота для наград за уровень")]
    [SerializeField] private CurrencyData _gold;

    [Tooltip("Ссылка на систему ресурсов")]
    [SerializeField] private ResourceSystem _resourceSystem;

    [Tooltip("Текущий уровень игрока")]
    public int currentLevel = 1;

    [Tooltip("Текущий опыт игрока")]
    public int currentXP;

    //Объяснение: событие при изменении уровня или опыта
    public event System.Action<int> OnLevelChanged;

    private void Start()
    {
        OnLevelChanged?.Invoke(currentLevel);
    }

    //Вызов: Добавляет опыт и проверяет повышение уровня
    public void AddXP(int amount)
    {
        if (amount <= 0)
            return;

        currentXP += amount;
        TryLevelUp();
        OnLevelChanged?.Invoke(currentLevel);
    }

    //Вызов: Возвращает долю заполнения полоски опыта (0..1)
    public float GetXpFillAmount()
    {
        LevelConfigRow next = FindLevelRow(currentLevel + 1);
        if (next == null)
            return 1f;

        int minXp = currentLevel <= 1 ? 0 : (FindLevelRow(currentLevel)?.requiredXP ?? 0);
        int maxXp = next.requiredXP;

        return maxXp <= minXp ? 1f : Mathf.Clamp01((float)(currentXP - minXp) / (maxXp - minXp));
    }

    //Вызов: Проверяет и применяет повышение уровня
    private void TryLevelUp()
    {
        if (_configLoader == null)
            return;

        LevelConfigRow nextLevel = FindLevelRow(currentLevel + 1);

        if (nextLevel != null && currentXP >= nextLevel.requiredXP)
        {
            currentLevel = nextLevel.level;
            _resourceSystem.AddResource(_gold, nextLevel.reward, "level_reward");
        }
    }

    //Вызов: Ищет строку уровня в загруженном CSV
    private LevelConfigRow FindLevelRow(int level)
    {
        foreach (LevelConfigRow row in _configLoader.Levels)
        {
            if (row.level == level)
                return row;
        }

        return null;
    }

    //Вызов: Принудительно обновляет UI уровня и полоски опыта
    public void NotifyLevelChanged()
    {
        OnLevelChanged?.Invoke(currentLevel);
    }

    //Вызов: Применяет сохранённый прогресс уровня
    public void ApplySave(int level, int xp)
    {
        currentLevel = level;
        currentXP = xp;
        OnLevelChanged?.Invoke(currentLevel);
    }
}
