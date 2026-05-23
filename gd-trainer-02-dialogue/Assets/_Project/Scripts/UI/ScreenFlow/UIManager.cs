using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [Tooltip("Ссылка на окна с компонентом Window")]
   [SerializeField] private Window[] _windows;
   [Tooltip("Ссылка на компонент Window Transition")]
   [SerializeField] private WindowTransition _windowTransition;
   
   [Tooltip("Ссылка на компонент AudioManager")]
   [SerializeField] private AudioManager _audioManager;
   
   //Вызов: Находит окно по имени и Отображает его
   public void ShowWindow(string windowName)
   {
      _windowTransition.SetAction(() =>
      {
         foreach (var item in _windows)
         {
            if(item.GetWindowName().Equals(windowName))
               item.Show();
         }
      });
   }
   
   //Вызов: Находит окно по имени и Скрывает его
   public void HideWindow(string windowName)
   {
      _windowTransition.SetAction(() =>
      {
         foreach (var item in _windows)
         {
            if(item.GetWindowName().Equals(windowName))
               item.Hide();
         }
      });
   }

   //Вызов: Проигрывает звук
   public void OnClickPlaySound(AudioClip clip)
   {
      if(clip != null)
         _audioManager.playSound?.Invoke(clip);
   }

   //Вызов: Закрывает игру (не работает в Editor)
   public void QuitGame()
   {
      Application.Quit();
   }
}
