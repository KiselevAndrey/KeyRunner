using CodeBase.UI.Visibility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(CanvasGroupController), typeof(MenuMediator), typeof(PressEscBehaviour))]
    public class MenuBehaviour : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;

        private CanvasGroupController _visibility;
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

        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
            _mediator = GetComponent<MenuMediator>();
            _pressEscBehaviour = GetComponent<PressEscBehaviour>();
        }

        private void Start()
        {
            _pressEscBehaviour.Init(ShowMenu);
        }

        private void OnEnable()
        {
            _visibility.Showed += ShowMenu;
            _startButton.onClick.AddListener(OnClickStartGame);
            _optionsButton.onClick.AddListener(OnClickOptions);
        }

        private void OnDisable()
        {
            _visibility.Showed -= ShowMenu;
            _startButton.onClick.RemoveListener(OnClickStartGame);
            _optionsButton.onClick.RemoveListener(OnClickOptions);
        }

        private void OnClickStartGame()
        {
            _mediator.Show(MenuWindow.SelectLVL);
            _pressEscBehaviour.enabled = true;
        }

        private void OnClickOptions()
        {
            //_mediator.Show(MenuWindow.Options);
            print("OnClickOptions");
        }
    }
}