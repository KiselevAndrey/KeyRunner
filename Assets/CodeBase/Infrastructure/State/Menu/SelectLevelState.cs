using CodeBase.UI;
using CodeBase.UI.Menu;

namespace CodeBase.Infrastructure.State.Menu
{
    public class SelectLevelState : IState
    {
        private MenuStateMachine _menuStateMachine;
        private MenuMediator _menuMediator;
        private PressEscService _escService;

        public SelectLevelState(MenuStateMachine menuStateMachine, MenuMediator menuMediator, PressEscService escService)
        {
            _menuStateMachine = menuStateMachine;
            _menuMediator = menuMediator;
            _escService = escService;
        }

        public void Enter()
        {
            _menuMediator.Show(MenuWindow.SelectLVL);
            _escService.enabled = true;
        }

        public void Exit()
        {
            _escService.enabled = false;
        }
    }
}