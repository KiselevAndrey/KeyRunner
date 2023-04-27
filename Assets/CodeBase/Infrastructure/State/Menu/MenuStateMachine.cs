namespace CodeBase.Infrastructure.State.Menu
{
    public class MenuStateMachine : AbstractStateMachine
    {
        public MenuStateMachine(UI.Menu.MenuMediator menuMediator, Service.AllServices container)
        {
            States = new()
            {
                [typeof(ButtonState)] = new ButtonState(this, menuMediator)
            };
        }
    }
}