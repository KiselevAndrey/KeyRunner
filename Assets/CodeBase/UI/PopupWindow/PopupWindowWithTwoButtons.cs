using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.PopupWindow
{
    public abstract class PopupWindowWithTwoButtons : PopupWindowAbstract
    {
        [Header("Buttons")]
        [SerializeField] Button _leftButton;
        [SerializeField] Button _rightButton;

        private UnityAction _onLeftAction;
        private UnityAction _onRightAction;

        public void Show(UnityAction onLeftAction, UnityAction onRightAction)
        {
            _onLeftAction = onLeftAction;
            _onRightAction = onRightAction;

            Show();
        }

        protected override void Subscribe()
        {
            _leftButton.onClick.AddListener(OnLeftButtonClick);
            _rightButton.onClick.AddListener(OnRightButtonClick);
        }

        protected override void Unsubscribe()
        {
            _leftButton.onClick.RemoveListener(OnLeftButtonClick);
            _rightButton.onClick.RemoveListener(OnRightButtonClick);
        }

        private void OnLeftButtonClick()
        {
            Hide();
            _onLeftAction?.Invoke();
        }

        private void OnRightButtonClick()
        {
            Hide();
            _onRightAction?.Invoke();
        }
    }
}