using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.PopupWindow
{
    public abstract class PopupWindowWithOneButton : PopupWindowAbstract
    {
        [Header("Buttons")]
        [SerializeField] Button _okButton;

        private UnityAction _okAction;

        public virtual void Show(UnityAction action)
        {
            _okAction = action;

            Show();
        }

        protected override void Subscribe()
        {
            _okButton.onClick.AddListener(OnOkButtonClick);
        }

        protected override void Unsubscribe()
        {
            _okButton.onClick.RemoveListener(OnOkButtonClick);
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Return))
                OnOkButtonClick();
        }

        private void OnOkButtonClick()
        {
            Hide();
            _okAction?.Invoke();
        }
    }
}