using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Скорость горизонтального движения персонажа")]
    [SerializeField] private float _moveSpeed = 8;

    [Tooltip("Сила прыжка персонажа")]
    [SerializeField] private float _jumpForce = 5;

    [Tooltip("Максимальное здоровье игрока")]
    [SerializeField] public float _health = 10;

    [Tooltip("На сколько поднять игрока над чекпоинтом при респауне")]
    [SerializeField] private float _checkpointYOffset = 1;

    private PlayerAnimator _animator;
    private PlayerMovement _movement;

    private List<Checkpoint> _checkpoints = new List<Checkpoint>();

    public float _sessionHealth;

    private void Awake()
    {
        // Запоминаем стартовое здоровье на текущую попытку прохождения
        _sessionHealth = _health;

        // Получаем ссылки на компоненты движения и анимации
        _movement = GetComponent<PlayerMovement>();
        _movement.Init(_moveSpeed, _jumpForce);

        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        // Выбираем индекс анимации: 0 — idle, 1 — run, 2 — jump
        int index = 0;
        if (_movement.isJump || !_movement.isGround)
            index = 2;
        else if (_movement.isMoving)
            index = 1;

        _animator.PlayAnimation(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Сохраняем пройденный чекпоинт, если ещё не сохраняли
        if (collision.TryGetComponent(out Checkpoint checkpoint))
        {
            if (!_checkpoints.Contains(checkpoint))
                _checkpoints.Add(checkpoint);
        }

        TryDealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Пока игрок стоит в опасной зоне, продолжаем проверять урон
        TryDealDamage(collision);
    }

    private void TryDealDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hazard hazard))
            hazard.DealDamage(this);

        if (collision.TryGetComponent(out Spike spike))
            spike.DealDamage(this);
    }

    // Вызов: наносит урон игроку и при нуле здоровья отправляет на чекпоинт
    public void TakeDamage(float damage)
    {
        _sessionHealth -= damage;

        if (_sessionHealth <= 0)
            RespawnAtCheckpoint();
    }

    private void RespawnAtCheckpoint()
    {
        // Восстанавливаем здоровье перед респауном
        _sessionHealth = _health;

        if (_checkpoints.Count == 0)
            return;

        // Телепортируем на последний сохранённый чекпоинт
        Checkpoint checkpoint = _checkpoints[_checkpoints.Count - 1];
        transform.position = new Vector2(checkpoint.transform.position.x,checkpoint.transform.position.y + _checkpointYOffset);
    }
}
