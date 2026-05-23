using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Tooltip("Переменная монет")]
    public int gold;
    [Tooltip("Переменная уровня репутации")]
    public float reputation;
    [Tooltip("Переменная массива флагов")]
    public string[] flags;

    //Вызов: Меняет базовое значение монет пример: 1 += 1 = 2
    public void ChangeGold(float value)
    {
        gold += (int)value;
        StatsUI.onChangeCoins?.Invoke(gold);
    }
    //Вызов: Меняет базовое значение уровня Репутации пример: 1 += 1 = 2
    public void ChangeReputation(float value)
    {
        reputation += value;
        StatsUI.onChangeReputation?.Invoke(reputation);
    }
}
