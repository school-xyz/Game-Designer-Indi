using System;
using UnityEngine;

public enum ConditionType
{
    None,
    HasGold,
    HasReputation,
    HasFlag
}

[Serializable]
public class Condition
{ 
    //Объяснение: Тип условия
   public ConditionType conditionType;
   //Объяснение: Требуемое количество значения
   public int requiredValue;
   //Объяснение: Флаг условия
   public string flagName;
}
