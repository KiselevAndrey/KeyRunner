using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase.UI.Menu
{
    public enum MenuWindow { Buttons, SelectLVL, Options }

    public class MenuMediator : CanvasMediator
    {
        [SerializeField] private CanvasGroupController _buttons;
        [SerializeField] private CanvasGroupController _selectLVL;
        [SerializeField] private CanvasGroupController _options;

        private MenuWindow _selected;

        public void Show(MenuWindow window)
        {
            ShowHide(false);
            _selected = window;
            ShowHide(true);
        }

        protected override void ShowHide(bool isShow)
        {
            switch (_selected)
            {
                case MenuWindow.Buttons:
                    ShowHide(_buttons, isShow);
                    break;
                case MenuWindow.SelectLVL:
                    ShowHide(_selectLVL, isShow);
                    break;
                case MenuWindow.Options:
                    ShowHide(_options, isShow);
                    break;
                default:
                    break;
            }
        }
    }
}