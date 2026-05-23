using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
   [Tooltip("Image для отображения Персонажа")]
   [SerializeField] private Image _speakerImage;
   [Tooltip("Label для отображения имени Персонажа")]
   [SerializeField] private TMP_Text _speakerLabel;
   [Tooltip("Text для отображения текста Персонажа")]
   [SerializeField] private TMP_Text _descriptionLabel;
   [Tooltip("Transform контейнер для кнопок выбора")]
   [SerializeField] private Transform _opinionBar;
   [Tooltip("Ссылка на префаб кнопки выбора")]
   [SerializeField] private Button _opinionButtonPrefab;
   [Tooltip("Ссылка на Image фонового Изображения")]
   [SerializeField] private Image _backgroundImage;
   [Tooltip("Ссылка на Диалоговый менеджер")]
   [SerializeField] private DialogueManager _dialogueManager;

   private void Awake()
   {
      _dialogueManager.OnNodeChanged += UpdateUI;
   }

   private void OnDestroy()
   {
      _dialogueManager.OnNodeChanged -= UpdateUI;
   }

   private void Start()
   { 
      // Первичная отрисовка текущей диалоговой ноды
      UpdateUI(_dialogueManager.CurrentNode);
   }

   private void UpdateUI(DialogueNode node)
   {
      // Удаляем старые кнопки выбора перед созданием новых
      foreach (Transform item in _opinionBar)
         Destroy(item.gameObject);

      // Обновляем имя говорящего и текст реплики
      _speakerLabel.text = node.speakerName;
      _descriptionLabel.text = node.dialogueText;

      // Если у ноды есть фон, устанавливаем его
      if (node.locationSprite != null)
         _backgroundImage.sprite = node.locationSprite;

      // Создаём кнопки для всех доступных вариантов ответа
      for (int i = 0; i < node.choices.Count; i++)
      {
         int index = i;
         Choice choice = node.choices[index];

         // Пропускаем вариант, если условие показа не выполнено
         if (!_dialogueManager.CanShowChoice(choice))
            continue;

         // Создаём кнопку выбора
         Button newButton = Instantiate(_opinionButtonPrefab, _opinionBar);

         // Устанавливаем текст кнопки
         TMP_Text label = newButton.GetComponentInChildren<TMP_Text>();
         if (label != null)
            label.text = choice.text;

         // Назначаем действие при нажатии на кнопку
         newButton.onClick.AddListener(() =>
         {
            // Если задано изображение реакции, обновляем портрет персонажа
            if (choice.reactionSprite != null)
               _speakerImage.sprite = choice.reactionSprite;
            // Передаём выбранный вариант в DialogueManager
            _dialogueManager.SelectChoice(index);
         });
      }
   }
}
