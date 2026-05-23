using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Dialogue Node", menuName = "Dialogue Node")]
public class DialogueNode : ScriptableObject
{ 
   [Tooltip("Фоновое изображение текущей сцены")]
   public Sprite locationSprite;
   [Tooltip("Имя персонажа, который говорит текущую реплику")]
   public string speakerName;
   [Tooltip("Текст текущей реплики")]
   public string dialogueText;
   [Tooltip("Список вариантов ответа игрока")]
   public List<Choice> choices;
}

[Serializable]
public struct Choice
{
   [Tooltip("Текст кнопки выбора")]
   public string text;
   [Tooltip("Изображение персонажа после выбора этого варианта")]
   public Sprite reactionSprite;
   [Tooltip("Следующая диалоговая нода")]
   public DialogueNode nextNode;
   [Tooltip("Условие показа этого варианта ответа")]
   public Condition condition;
   [Tooltip("Эффект, который применится после выбора")]
   public ChoiceEffect effect;
}

public enum ChoiceEffectType
{
   Increase,
   Decrease,
   Menu,
}

[Serializable]
public struct ChoiceEffect
{
   [Tooltip("Параметр игрока, который будет изменён")]
   public ConditionType conditionType;
   [Tooltip("Тип эффекта: увеличить, уменьшить или перейти в меню")]
   public ChoiceEffectType choiceEffectType;
   [Tooltip("Значение изменения")]
   public float value;
}
