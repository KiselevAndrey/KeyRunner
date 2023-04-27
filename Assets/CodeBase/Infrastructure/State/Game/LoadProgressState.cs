using CodeBase.StatisticData;

namespace CodeBase.Infrastructure.State.Game
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            // —читывание инфы

            _sceneLoader.Load(SceneName.Menu, OnSceneLoaded);
        }

        public void Exit() { }

        private void OnSceneLoaded() =>
            _gameStateMachine.Enter<MenuState>();
    }
}