using CodeBase.Infrastructure.State.Game;
using CodeBase.UI.Game;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}