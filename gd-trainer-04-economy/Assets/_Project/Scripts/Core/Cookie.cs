using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cookie : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Максимальный масштаб при клике")]
    [Range(1, 5)]
    [SerializeField] private float _maxScale;
    [Tooltip("Максимальный поворот при клике")]
    [Range(0, 360)]
    [SerializeField] private float _maxRotation;
    [Tooltip("Длительность анимации клика")]
    [Range(0, 3)]
    [SerializeField] private float _animationTime;

    private Vector3 _scale;
    private Quaternion _rotation;
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        _scale = transform.localScale;
        _rotation = transform.rotation;
    }

    //Вызов: Обрабатывает клик игрока по печеньке
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);

        _animationCoroutine = StartCoroutine(ClickAnim());
        GameEvents.OnCookieClicked?.Invoke();
    }

    //Вызов: Проигрывает анимацию клика
    private IEnumerator ClickAnim()
    {
        yield return Animate(_scale * Random.Range(1, _maxScale),
            Quaternion.Euler(_rotation.x, _rotation.y, _rotation.z + Random.Range(-_maxRotation, _maxRotation)),
            _animationTime);
        yield return Animate(_scale, _rotation, _animationTime);
        _animationCoroutine = null;
    }

    //Вызов: Плавно меняет масштаб и поворот объекта
    private IEnumerator Animate(Vector3 targetScale, Quaternion targetRotation, float time)
    {
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            float e = 1 - Mathf.Pow(1 - t, 3);
            transform.localScale = Vector3.Lerp(startScale, targetScale, e);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, e);
            yield return null;
        }

        transform.localScale = targetScale;
        transform.rotation = targetRotation;
    }
}
