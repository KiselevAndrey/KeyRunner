using CodeBase.Settings;
using CodeBase.Settings.Singleton;
using CodeBase.UI.Keyboard;
using CodeBase.UI.Visibility;
using CodeBase.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(CanvasGroupController))]
    public class SelectLvlBehaviour : MonoBehaviour
    {
        [Header("SO")]
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;

        [Header("Pool")]
        [SerializeField] private SelectLvlButton _selectLvlButtonPrefab;
        [SerializeField] private LocalPool _lvlPool;

        [Header("Other")]
        [SerializeField] private KeyboardBehaviour _keyboard;
        [SerializeField] private MenuBehaviour _menu;
        [SerializeField] private Button _escButton;

        private CanvasGroupController _visibility;

        #region Unity Lifecycle
        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
        }

        private void Start()
        {
            for (int i = 0; i < _levelsOfKeysSO.Levels.Count; i++)
            {
                var lvlButton = _lvlPool.Spawn(_selectLvlButtonPrefab);
                //var lvlInfo = _levelsOfKeysSO.Levels[i];
                lvlButton.Init(i + 1, ShowLvlKeys, OnLevelSelect);
            }

            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        private void OnEnable()
        {
            _visibility.Showing += OnShowing;
            _escButton.onClick.AddListener(OnEscButtonClick);
        }

        private void OnDisable()
        {
            _visibility.Showing -= OnShowing;
            _escButton.onClick.RemoveListener(OnEscButtonClick);
        }
        #endregion Unity Lifecycle

        #region Private
        private void ShowLvlKeys(int level)
        {
            level--;
            _keyboard.DeselectAllDisplays();
            _keyboard.ResetShift();

            foreach (var info in _levelsOfKeysSO.GenerateKeys(level))
            {
                _keyboard.PaintKey(info.FirstKeys, Color.yellow);
                _keyboard.PaintKey(info.SecondKeys, Color.yellow);
            }

            var newKeys = _levelsOfKeysSO.GetNewKeys(level)[0];
            if (newKeys.IsShifted)
                _keyboard.AddShift();

            _keyboard.PaintKey(newKeys.FirstKeys, Color.red);
            _keyboard.PaintKey(newKeys.SecondKeys, Color.red);
        }

        private void OnLevelSelect(int level)
        {
            level--;
            PlayerInfoSO.SelectedLVL = level;
            _menu.StartingGame();
        }

        private void OnShowing()
        {
            _keyboard.DeselectAllDisplays();
        }

        private void OnEscButtonClick()
        {
            _menu.ShowMenu();
        }
        #endregion Private
    }
}