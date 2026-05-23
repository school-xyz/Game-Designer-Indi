using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("Ссылка на начальную диалоговую ноду")]
    [SerializeField] private DialogueNode _startNode;
    [Tooltip("Ссылка на Game Manager компонент")]
    [SerializeField] private GameManager _gameManager;
    [Tooltip("Звук перемещениями диалогами")]
    [SerializeField] private AudioClip _clickSound;
    
    private DialogueNode _currentNode;

    public DialogueNode CurrentNode => _currentNode;
    
    public event Action<DialogueNode> OnNodeChanged;
    
    private void Start()
    {
        Restart();
    }
    // Вызов: перезапускает диалог с начальной ноды
    public void Restart()
    {   
        _gameManager.GetPlayerState().ChangeReputation(0);
        _gameManager.GetPlayerState().ChangeGold(0);
        _currentNode = _startNode;
        OnNodeChanged?.Invoke(_currentNode);
    }
    // Вызов: проверяет, можно ли показать вариант ответа игроку
    public bool CanShowChoice(Choice choice)
    {
        PlayerState playerState = _gameManager.GetPlayerState();

        switch (choice.condition.conditionType)
        {
            case ConditionType.HasGold:
                return playerState.gold >= choice.condition.requiredValue;
            case ConditionType.HasReputation:
                return playerState.reputation >= choice.condition.requiredValue;
            default:
                return true;
        }
    }
    // Вызов: обрабатывает выбранный игроком вариант ответа
    public void SelectChoice(int index)
    {
        Choice choice = _currentNode.choices[index];
        _gameManager.GetUIManager().OnClickPlaySound(_clickSound);
        ApplyEffect(choice.effect);

        if(choice.nextNode == null)
            return;
        _currentNode = choice.nextNode;
        OnNodeChanged?.Invoke(_currentNode);
    }
    // Вызов: применяет эффект выбранного варианта
    private void ApplyEffect(ChoiceEffect effect)
    {
        switch (effect.choiceEffectType)
        {
            case ChoiceEffectType.Increase:
                ApplyValue(effect.conditionType, effect.value);
                break;
            case ChoiceEffectType.Decrease:
                ApplyValue(effect.conditionType, -effect.value);
                break;
            case ChoiceEffectType.Menu:
                _gameManager.GetUIManager().ShowWindow("Menu");
                _gameManager.GetUIManager().HideWindow("Game");
                Restart();
                break;  
        }
    }
    // Вызов: изменяет золото или репутацию игрока
    private void ApplyValue(ConditionType type, float value)
    {
        PlayerState playerState = _gameManager.GetPlayerState();

        switch (type)
        {
            case ConditionType.HasGold:
                playerState.ChangeGold(value);
                break;

            case ConditionType.HasReputation:
                playerState.ChangeReputation(value);
                break;
        }
    }
}
