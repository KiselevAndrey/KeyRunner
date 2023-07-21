using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.PopupWindow
{
    public class PopupWindowWithTwoButtonsAndText : PopupWindowWithTwoButtons
    {
        [Header("Text")]
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        public void Show(string header, string body, UnityAction onLeftAction, UnityAction onRightAction)
        {
            _header.text = header;
            _body.text = body;

            Show(onLeftAction, onRightAction);
        }
    }
}