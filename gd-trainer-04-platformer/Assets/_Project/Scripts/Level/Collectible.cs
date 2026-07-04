using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Количество очков/монет, которое даёт предмет")]
    [SerializeField] private int _coinValue = 1;

    private bool _collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что предмет подобрал именно игрок
        if (collision.GetComponentInParent<PlayerController>() == null)
            return;
        if (_collected)
            return;
        _collected = true;
        if (GameManager.Instance != null)
            GameManager.Instance.AddCollectible(_coinValue);

        // Уничтожаем предмет после подбора
        Destroy(gameObject);
    }
}
