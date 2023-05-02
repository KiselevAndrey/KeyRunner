using CodeBase.UI;
using CodeBase.UI.Menu;

namespace CodeBase.Infrastructure.State.Menu
{
    public class MenuStateMachine : AbstractStateMachine
    {
        public MenuStateMachine(MenuMediator menuMediator, PressEscService escService, Service.AllServices container)
        {
            States = new()
            {
                [typeof(ButtonState)] = new ButtonState(this, menuMediator, escService),
                [typeof(SelectLevelState)] = new SelectLevelState(this, menuMediator, escService),
                [typeof(OptionsState)] = new OptionsState(this, menuMediator, escService),
                [typeof(LeaderboardState)] = new LeaderboardState(this, menuMediator, escService)
            };
        }
    }
}