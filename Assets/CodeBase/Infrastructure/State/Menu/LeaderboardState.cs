using CodeBase.UI.Menu;
using CodeBase.UI;

namespace CodeBase.Infrastructure.State.Menu
{
    public class LeaderboardState : IState
    {
        private MenuStateMachine _menuStateMachine;
        private MenuMediator _menuMediator;
        private PressEscService _escService;

        public LeaderboardState(MenuStateMachine menuStateMachine, MenuMediator menuMediator, PressEscService escService)
        {
            _menuStateMachine = menuStateMachine;
            _menuMediator = menuMediator;
            _escService = escService;
        }

        public void Enter()
        {
            _menuMediator.Show(MenuWindow.Options);
            _escService.enabled = true;
        }

        public void Exit()
        {
            _escService.enabled = false;
        }
    }
}