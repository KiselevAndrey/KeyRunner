using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(MenuMediator), typeof(PressEscBehaviour))]
    public class MenuForm : UIForm
    {
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;

        private MenuMediator _mediator;
        private PressEscBehaviour _pressEscBehaviour;

        public event UnityAction StartGame;

        public void StartingGame()
        {
            ShowMenu();
            StartGame?.Invoke();
        }

        public void ShowMenu()
        {
            _mediator.Show(MenuWindow.Buttons);
            _pressEscBehaviour.enabled = false;
        }

        public void EnableEsc()
            => _pressEscBehaviour.enabled = true;

        #region Protected
        protected override void OnShowed()
            => ShowMenu();

        protected override void OnAwake()
        {
            _mediator = GetComponent<MenuMediator>();
            _pressEscBehaviour = GetComponent<PressEscBehaviour>();
        }

        protected override void OnStarted() 
            => _pressEscBehaviour.Init(ShowMenu);

        protected override void Subscribe()
        {
            _startButton.onClick.AddListener(OnClickStartGame);
            _optionsButton.onClick.AddListener(OnClickOptions);
        }

        protected override void Unsubscribe()
        {
            _startButton.onClick.RemoveListener(OnClickStartGame);
            _optionsButton.onClick.RemoveListener(OnClickOptions);
        }
        #endregion Protected

        private void OnClickStartGame() 
            => _mediator.Show(MenuWindow.SelectLVL);

        private void OnClickOptions()
        {
            _mediator.Show(MenuWindow.Options);
            print("OnClickOptions");
        }
    }
}