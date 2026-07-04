using UnityEngine;

// Компонент финиша уровня
public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что до финиша дошёл именно игрок
        if (collision.GetComponentInParent<PlayerController>() == null)
            return;

        if (GameManager.Instance != null)
            GameManager.Instance.CompleteLevel();
    }
}
