using UnityEngine;

public class Window : MonoBehaviour
{
   [Tooltip("Имя окна используемый для отображения и скрытия через UIManager")]
   [SerializeField] private string windowName;
   
   //Вызов: Возвращает значение переменной windowName
   public string GetWindowName() => windowName;
   //Вызов: Отображает игровой объект
   public void Show()
   {
      gameObject.SetActive(true);
   }
   //Вызов: Скрывает игровой объект
   public void Hide()
   {
      gameObject.SetActive(false);
   }
}
