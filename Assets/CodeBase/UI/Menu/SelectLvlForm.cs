using CodeBase.Settings;
using CodeBase.Settings.Singleton;
using CodeBase.UI.Keyboard;
using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.UI.Menu
{
    public class SelectLvlForm : MenuSubform
    {
        [Header("SO")]
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;

        [Header("Pool")]
        [SerializeField] private SelectLvlButton _selectLvlButtonPrefab;
        [SerializeField] private LocalPool _lvlPool;

        [Header("Other")]
        [SerializeField] private KeyboardService _keyboard;

        protected override void OnShowing()
        {
            base.OnShowing();
            _keyboard.DeselectAllDisplays();
        }

        protected override void OnStarted()
        {
            for (int i = 0; i <= _levelsOfKeysSO.MaxLevel; i++)
            {
                var lvlButton = _lvlPool.Spawn(_selectLvlButtonPrefab);
                lvlButton.Init(i + 1, ShowLvlKeys, OnLevelSelect);
            }

            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        private void ShowLvlKeys(int level)
        {
            level--;
            _keyboard.DeselectAllDisplays();
            _keyboard.ResetShift();

            foreach (var info in _levelsOfKeysSO.GenerateKeys(level))
            {
                _keyboard.PaintKey(info.FirstKey, Color.yellow);
                _keyboard.PaintKey(info.SecondKey, Color.yellow);
            }

            var newKeys = _levelsOfKeysSO.GetNewKeys(level)[0];
            if (newKeys.IsShifted)
                _keyboard.AddShift();

            _keyboard.PaintKey(newKeys.FirstKey, Color.red);
            _keyboard.PaintKey(newKeys.SecondKey, Color.red);
        }

        private void OnLevelSelect(int level)
        {
            level--;
            PlayerInfoSO.SelectedLVL = level;
            Menu.StartingGame();
        }
    }
}