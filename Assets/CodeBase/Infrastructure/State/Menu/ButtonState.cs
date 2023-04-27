using CodeBase.UI.Menu;

namespace CodeBase.Infrastructure.State.Menu
{
    public class ButtonState : IState
    {
        private readonly MenuStateMachine _menuStateMachine;
        private readonly MenuMediator _menuMediator;

        public ButtonState(MenuStateMachine menuStateMachine, MenuMediator menuMediator) 
        {
            _menuStateMachine = menuStateMachine;
            _menuMediator = menuMediator;
        }

        public void Enter()
        {
            _menuMediator.Show(MenuWindow.Buttons);
        }

        public void Exit()
        {
        }
    }
}