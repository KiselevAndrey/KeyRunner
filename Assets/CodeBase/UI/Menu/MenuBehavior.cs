using CodeBase.UI.Visibility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(CanvasGroupController), typeof(MenuMediator))]
    public class MenuBehavior : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _selectLvlButton;
        [SerializeField] private Button _optionsButton;

        private CanvasGroupController _visibility;
        private MenuMediator _mediator;

        public event UnityAction StartGame;

        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
            _mediator = GetComponent<MenuMediator>();
        }

        private void OnEnable()
        {
            _visibility.Showed += OnShowed;
            _startButton.onClick.AddListener(OnClickStartGame);
            _selectLvlButton.onClick.AddListener(OnClickSelectLVL);
            _optionsButton.onClick.AddListener(OnClickOptions);
        }

        private void OnDisable()
        {
            _visibility.Showed -= OnShowed;
            _startButton.onClick.RemoveListener(OnClickStartGame);
            _selectLvlButton.onClick.RemoveListener(OnClickSelectLVL);
            _optionsButton.onClick.RemoveListener(OnClickOptions);
        }

        private void OnShowed()
        {
            _mediator.Show(MenuWindow.Buttons);
        }

        private void OnClickStartGame()
        {
            StartGame?.Invoke();
        }

        private void OnClickSelectLVL()
        {
            //_mediator.Show(MenuWindow.SelectLVL);
            print("OnClickSelectLVL");
        }

        private void OnClickOptions()
        {
            //_mediator.Show(MenuWindow.Options);
            print("OnClickOptions");
        }
    }
}