using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [Tooltip("Ссылка на текстовый компонент TMP_Text")]
    public TMP_Text scoreLabel = null;

    //Метод отвечающий зха обновления текста
    public void UpdateScoreLabel(string value)
    {
        if(scoreLabel != null)
            scoreLabel.text = value;
    }
}
