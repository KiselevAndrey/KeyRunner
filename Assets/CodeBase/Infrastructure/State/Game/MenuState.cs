using CodeBase.StatisticData;
using CodeBase.UI.Game;
using UnityEngine;

namespace CodeBase.Infrastructure.State.Game
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public MenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(SceneName.Menu, OnSceneLoaded);
        }

        public void Exit() { }


        private void OnSceneLoaded()
        {
            Debug.Log("OnMenuSceneLoaded");
            _curtain.Hide();
        }
    }
}
