using System;
using UnityEngine;

public class WindowTransition : MonoBehaviour
{
    private Action _action;
    
    //Вызов: Вызывает action при вызове и обнуляет его
    public void CallAction()
    {
        _action?.Invoke();  
        _action =  null;    
    }

    //Вызов: Скрывает игровой объект
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //Вызов: подписываем Action на локальный action и Отображаем игровой объект
    public void SetAction(Action action)
    {
        gameObject.SetActive(true);
        _action += action;
    }
}
