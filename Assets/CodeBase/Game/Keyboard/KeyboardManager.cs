using CodeBase.Game.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Keys = CodeBase.Utility.KeyboardInputUtility;

namespace CodeBase.Game.Keyboard
{
    public class KeyboardManager : MonoBehaviour
    {
        [SerializeField] private LanguageKeyMapSO _keyMapSO;

        private KeyCode _pressedKey;
        private KeyDisplay _selectedDisplay;
        private List<KeyDisplay> _keys = new();

        public void InitKeys(LanguageKeyMapSO keyMapSO)
        {
            foreach (var key in _keys)
            {
                key.Init(keyMapSO.GetInfo(key.Key));
            }
        }

        private void Awake()
        {
            _keys = GetComponentsInChildren<KeyDisplay>().ToList();
        }

        private void Start()
        {
            InitKeys(_keyMapSO);
        }

        private void Update()
        {
            if (Keys.KeyDown(ref _pressedKey))
                print(_pressedKey);

            if (Keys.KeyPressed(ref _pressedKey))
                SelectKeyDisplay();
            else
                DeselectSelectedKeys();
        }

        private void SelectKeyDisplay()
        {
            if (_selectedDisplay != null && _selectedDisplay.Key == _pressedKey)
                return;

            DeselectSelectedKeys();
            _selectedDisplay = FindDisplay(_pressedKey);

            if (_selectedDisplay == null)
                return;

            _selectedDisplay.ChangeColor(Color.yellow);
        }

        private void DeselectSelectedKeys()
        {
            if (_selectedDisplay == null)
                return;

            _selectedDisplay.ChangeColor(Color.white);
            _selectedDisplay = null;
        }

        private KeyDisplay FindDisplay(KeyCode key)
        {
            int displayIndex = _keys.FindIndex(d => d.Key == key);
            if (displayIndex < 0)
                return null;
            
            return _keys[displayIndex];
        }
    }
}