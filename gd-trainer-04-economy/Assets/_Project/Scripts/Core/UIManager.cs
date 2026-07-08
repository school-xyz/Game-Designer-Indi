using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("Тексты уровня игрока")]
    [SerializeField] private Text[] _levelTxt;

    [Tooltip("Текст награды за клик")]
    [SerializeField] private Text _clickRewardTxt;

    [Tooltip("Текст пассивного дохода")]
    [SerializeField] private Text _passiveIncomeTxt;

    [Tooltip("Ссылка на LevelSystem")]
    [SerializeField] private LevelSystem _levelSystem;

    private void Awake()
    {
        if (_levelSystem != null)
            _levelSystem.OnLevelChanged += UpdateLevelLabel;
    }

    private void OnDestroy()
    {
        if (_levelSystem != null)
            _levelSystem.OnLevelChanged -= UpdateLevelLabel;
    }

    //Вызов: Обновляет все элементы HUD
    public void RefreshAll(GameManager gameManager)
    {
        RefreshClickReward(gameManager.GetClickReward());
        RefreshPassiveIncome(gameManager.GetPassiveIncome());

        if (_levelSystem != null)
            UpdateLevelLabel(_levelSystem.currentLevel);
    }

    //Вызов: Обновляет текст награды за клик
    public void RefreshClickReward(int value)
    {
        if (_clickRewardTxt != null)
            _clickRewardTxt.text = "x" + value + "/CLICK";
    }

    //Вызов: Обновляет текст пассивного дохода
    public void RefreshPassiveIncome(int value)
    {
        if (_passiveIncomeTxt != null)
            _passiveIncomeTxt.text = "x" + value + "/SEC";
    }

    //Вызов: Обновляет текст уровня
    private void UpdateLevelLabel(int value)
    {
        if (_levelTxt != null)
        {
            foreach (Text item in _levelTxt)
                item.text = "LVL " + value;
        }
    }
}
