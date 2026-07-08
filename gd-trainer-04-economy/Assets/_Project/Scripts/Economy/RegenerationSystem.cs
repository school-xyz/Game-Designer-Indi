using UnityEngine;

public class RegenerationSystem : MonoBehaviour
{
    [Tooltip("Ресурс энергии (назначьте Energy.asset в Тренажёре 5.2)")]
    [SerializeField] private CurrencyData _energy;

    [Tooltip("Сколько единиц восстанавливается в секунду")]
    [SerializeField] private float _regenPerSecond = 1f;

    private float _timer;

    //Вызов: Постепенно восстанавливает энергию до максимума
    private void Update()
    {
        // ← ЗАДАНИЕ (Тренажёр 5.2): назначьте Energy.asset в _energy и проверьте регенерацию
        if (_energy == null || _energy.currentAmount >= _energy.maxAmount)
            return;

        _timer += Time.deltaTime;
        while (_timer >= 1f / _regenPerSecond)
        {
            _timer -= 1f / _regenPerSecond;
            _energy.SetAmount(_energy.currentAmount + 1);
        }
    }
}
