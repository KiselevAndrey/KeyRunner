using CodeBase.Settings;
using CodeBase.UI.Keyboard;
using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.UI.Menu
{
    public class SelectLvlBehavior : MonoBehaviour
    {
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;
        [SerializeField] private KeyboardBehavior _keyboard;
        [SerializeField] private SelectLvlButton _selectLvlButtonPrefab;
        [SerializeField] private LocalPool _lvlPool;
        [SerializeField] private MenuMediator _menuMediator;

        private void Start()
        {
            for (int i = 0; i < _levelsOfKeysSO.Levels.Count; i++)
            {
                var lvlButton = _lvlPool.Spawn(_selectLvlButtonPrefab);
                var lvlInfo = _levelsOfKeysSO.Levels[i];
                lvlButton.Init(i + 1, ShowLvlKeys, OnLevelSelect);
            }

            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        private void ShowLvlKeys(int level)
        {
            level--;
            _keyboard.DeselectAllDisplays();
            _keyboard.ResetShift();

            if (_levelsOfKeysSO.GetNewKeys(level)[0].IsShifted)
                _keyboard.AddShift();

            foreach (var info in _levelsOfKeysSO.GenerateKeys(level))
            {
                _keyboard.HighlightDisplay(info.FirstKeys, false);
                _keyboard.HighlightDisplay(info.SecondKeys, false);
            }
        }

        private void OnLevelSelect(int level)
        {
            level--;
            _menuMediator.Show(MenuWindow.Buttons);
        }
    }
}