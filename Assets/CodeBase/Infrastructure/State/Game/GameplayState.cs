using CodeBase.StatisticData;
using CodeBase.UI.Game;
using UnityEngine;

namespace CodeBase.Infrastructure.State.Game
{
    public class GameplayState : IState
    {
        private GameStateMachine _gameStrateMachine;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;

        public GameplayState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStrateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(SceneName.Gameplay, OnSceneLoaded);
        }

        public void Exit() { }

        private void OnSceneLoaded()
        {
            Debug.Log("OnGameplaySceneLoaded");
            _curtain.Hide();
        }
    }
}