using TMPro;
using UnityEngine;

namespace CodeBase.UI.PopupWindow
{
    public abstract class PopupWindowAbstractWithText : PopupWindowAbstract
    {
        [Header("Text")]
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        protected void Show(string header, string body)
        {
            _header.text = header;
            _body.text = body;

            Show();
        }
    }
}