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
        [SerializeField] private TMP_Text _scoreText;

        public override void Show(UnityAction action)
        {
            _gameTimeText.text = $"{(int)_gameStatisticsInfoSO.GameDuration / 60} : {(int)(_gameStatisticsInfoSO.GameDuration % 60)}";
            _speedText.text = _gameStatisticsInfoSO.PressedKeysPerMin.ToString();
            _mistakeText.text = _gameStatisticsInfoSO.MistakesPercent.ToString();
            _scoreText.text = _gameStatisticsInfoSO.Score.ToString();

            base.Show(action);
        }
    }
}