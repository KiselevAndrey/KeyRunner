using CodeBase.Gameplay;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Game;
using UnityEngine;

namespace CodeBase.UI.Gameplay
{
    [RequireComponent(typeof(PressEscService))]
    public class GameplayForm : MonoBehaviour, IGameplayKeyboardChecker
    {
        [SerializeField] private KeyboardAgent _keyboardAgent;

        private PressEscService _escService;
        private IGameplayService _gameplayService;
        private IGameplayInformer _gameplayInformer;

        private AllServices Container => AllServices.Container;

        public void Init(IGameplayService gameplayService, IGameplayInformer gameplayInformer)
        {
            _gameplayService = gameplayService;
            _gameplayInformer = gameplayInformer;
        }

        public bool IsRightKey(KeyCode key, bool isShiftPressed) =>
            _gameplayService.IsRightKey(key, isShiftPressed);

        #region Unity Lifecycle
        private void Awake()
        {
            _escService = GetComponent<PressEscService>();
            _escService.Init(GoToMenu);

            _keyboardAgent.Init(this);
        }

        private void OnEnable()
        {
            _keyboardAgent.OnEnable();
        }

        private void OnDisable()
        {
            _keyboardAgent.OnDisable();
        }
        #endregion Unity Lifecycle

        private void GoToMenu() =>
            Container.Single<GameStateMachine>().Enter<MenuState>();
    }
}
