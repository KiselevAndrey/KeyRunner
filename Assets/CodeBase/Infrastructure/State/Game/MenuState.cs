using CodeBase.UI.Logic;

namespace CodeBase.Infrastructure.State.Game
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _curtain;
        
        public MenuState(GameStateMachine gameStateMachine, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
        }
        
        public void Enter()
        {
            _curtain.Hide();
        }
        
        public void Exit() { }
    }
}
