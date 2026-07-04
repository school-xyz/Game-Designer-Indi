using UnityEngine;

public class Slime : MonoBehaviour
{
    [Tooltip("Множитель скорости (0.5 = в 2 раза медленнее)")]
    [SerializeField] private float _speedMultiplier = 0.5f;

    public float SpeedMultiplier => _speedMultiplier;
}
