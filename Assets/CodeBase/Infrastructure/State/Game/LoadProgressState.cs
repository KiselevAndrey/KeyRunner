namespace CodeBase.Infrastructure.State.Game
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            // TODO: —читывание инфы

            _gameStateMachine.Enter<MenuState>();
        }

        public void Exit() { }
    }
}