using CodeBase.UI;
using CodeBase.UI.Menu;

namespace CodeBase.Infrastructure.State.Menu
{
    public class ButtonState : IState
    {
        private readonly MenuStateMachine _menuStateMachine;
        private readonly MenuMediator _menuMediator;
        private readonly PressEscService _escService;

        public ButtonState(MenuStateMachine menuStateMachine, MenuMediator menuMediator, PressEscService escService) 
        {
            _menuStateMachine = menuStateMachine;
            _menuMediator = menuMediator;
            _escService = escService;
        }

        public void Enter()
        {
            _menuMediator.Show(MenuWindow.Buttons);
            _escService.enabled = false;
        }

        public void Exit() { }
    }
}