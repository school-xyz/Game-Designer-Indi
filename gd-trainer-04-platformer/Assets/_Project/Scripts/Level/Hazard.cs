using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Tooltip("Урон за один тик")]
    [SerializeField] private float _damage = 1f;

    [Tooltip("Интервал между нанесением урона в секундах")]
    [SerializeField] private float _damageInterval = 1f;

    private float _nextDamageTime;

    // Вызов: наносит урон игроку с заданным интервалом
    public void DealDamage(PlayerController player)
    {
        if (Time.time < _nextDamageTime)
            return;

        _nextDamageTime = Time.time + _damageInterval;
        player.TakeDamage(_damage);
    }
}
