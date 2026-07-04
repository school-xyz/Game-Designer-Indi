using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Conveyor : MonoBehaviour
{
    [Tooltip("Направление движения конвейера")]
    [SerializeField] private Vector2 _pushDirection = Vector2.right;

    [Tooltip("Скорость толчка конвейера по горизонтали")]
    [SerializeField] private float _pushSpeed = 4f;

    public float PushSpeed => _pushDirection.normalized.x * _pushSpeed;
}
