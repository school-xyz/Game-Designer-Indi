using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Tooltip("Ссылка на компонент Animator персонажа")]
    [SerializeField] private Animator _animator;

    // Имя int-параметра в Animator Controller
    private const string ANIMATION_KEY = "STATE";

    // Вызов: переключает анимацию по индексу (0 — idle, 1 — run, 2 — jump)
    public void PlayAnimation(int index)
    {
        _animator.SetInteger(ANIMATION_KEY, index);
    }
}
