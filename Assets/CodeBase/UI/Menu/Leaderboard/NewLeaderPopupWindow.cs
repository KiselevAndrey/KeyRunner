using CodeBase.Game.Statistics;
using CodeBase.UI.PopupWindow;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.Menu.Leaderboard
{
    public class NewLeaderPopupWindow : PopupWindowWithOneButton
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private GameStatisticsInfoSO _gameStatisticsInfoSO;

        private UnityAction<string> _selectNameAction;

        public void Show(UnityAction<string> selectNameAction)
        {
            _selectNameAction = selectNameAction;

            base.Show(null);
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            _nameInput.text = _gameStatisticsInfoSO.PlayerName;
            _nameInput.Select();
        }

        protected override void OnOkButtonClick()
        {
            base.OnOkButtonClick();

            _selectNameAction?.Invoke(_nameInput.text);
        }
    }
}