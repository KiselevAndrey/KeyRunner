using CodeBase.Game.Statistics;
using CodeBase.UI.PopupWindow;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.Game
{
    public class StatisticsPopupWindow : PopupWindowWithOneButton
    {
        [SerializeField] private GameStatisticsInfoSO _gameStatisticsInfoSO;
        [SerializeField] private TMP_Text _gameTimeText;
        [SerializeField] private TMP_Text _speedText;
        [SerializeField] private TMP_Text _mistakeText;

        public override void Show(UnityAction action)
        {
            _gameTimeText.text = $"{(int)_gameStatisticsInfoSO.GameDuration / 60} : {(int)(_gameStatisticsInfoSO.GameDuration % 60)}";
            _speedText.text = ((int)_gameStatisticsInfoSO.PressedKeysPerMin).ToString();
            _mistakeText.text = ((int)_gameStatisticsInfoSO.MistakesPercent).ToString();

            base.Show(action);
        }
    }
}