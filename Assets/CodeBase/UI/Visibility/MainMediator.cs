using UnityEngine;

namespace CodeBase.UI.Visibility
{
    public enum Window { Menu, Game }

    public class MainMediator : CanvasMediator
    {
        [SerializeField] private CanvasGroupController _menu;
        [SerializeField] private CanvasGroupController _game;

        private Window _selectedWindow;

        public void Show(Window window)
        {
            ShowHide(false);
            _selectedWindow = window;

            ShowHide(true);
        }

        protected override void ShowHide(bool isShow)
        {
            switch (_selectedWindow)
            {
                case Window.Menu:
                    ShowHide(_menu, isShow);
                    break;
                case Window.Game:
                    ShowHide(_game, isShow);
                    break;
                default:
                    break;
            }
        }
    }
}