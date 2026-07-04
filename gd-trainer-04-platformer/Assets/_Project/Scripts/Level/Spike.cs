using UnityEngine;

public class Spike : MonoBehaviour
{
    [Tooltip("Урон при касании шипов")]
    [SerializeField] private float _damage = 1f;

    private float _nextDamageTime;

    // Вызов: наносит урон игроку при контакте со шипами
    public void DealDamage(PlayerController player)
    {
        if (Time.time < _nextDamageTime)
            return;

        _nextDamageTime = Time.time + 0.1f;
        player.TakeDamage(_damage);
    }
}
