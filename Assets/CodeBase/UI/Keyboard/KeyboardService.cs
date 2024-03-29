using CodeBase.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Keys = CodeBase.Utility.KeyboardInputUtility;

namespace CodeBase.UI.Keyboard
{
    public class KeyboardService : MonoBehaviour
    {
        [SerializeField] private LanguageKeyMapSO _keyMapSO;

        private int _shiftPressed;

        private KeyDisplay _highlightedDisplay;
        private List<KeyDisplay> _displays = new();

        public event UnityAction<KeyCode, bool> PressedKey;

        #region Public
        public void InitDisplays(LanguageKeyMapSO keyMapSO)
        {
            foreach (var display in _displays)
                display.Init(keyMapSO.GetInfo(display.Key));

            ResetShift();
        }

        public void HighlightDisplay(KeyCode key)
        {
            if (_highlightedDisplay != null)
                _highlightedDisplay.ResetColor();

            _highlightedDisplay = SelectKey(key, Color.yellow);
        }

        public void DeselectAllDisplays()
        {
            foreach (var dispay in _displays)
                dispay.ResetColor();
        }

        public void ResetShift()
            => AddShift(-_shiftPressed);

        public void AddShift(int value = 1)
        {
            var shiftedNow = _shiftPressed > 0;

            _shiftPressed += value;

            if (shiftedNow != _shiftPressed > 0)
                foreach (var display in _displays)
                    display.SetShift(!shiftedNow);
        }

        public void PaintKey(KeyCode key, Color color)
            => SelectKey(key, color);
        #endregion Public

        private void Awake()
        {
            _displays = GetComponentsInChildren<KeyDisplay>().ToList();
        }

        private void Update()
        {
            if(Keys.KeysInfoDownUp(out List<KeyCode> downed, out List<KeyCode> uppened))
            {
                UpdateKeysDown(downed);
                UpdateKeysUp(uppened);
            }
        }

        #region Private
        private void UpdateKeysDown(List<KeyCode> keys)
        {
            foreach (var key in keys)
            {
                //print($"Key Down: {key}");
                var display = SelectKey(key, Color.red);

                if (display == null)
                    return;

                if (IsSupportiveKeys(key))
                {
                    if (IsShift(key))
                        AddShift();
                }
                else
                    PressedKey?.Invoke(key, _shiftPressed > 0);
            }    
        }

        private KeyDisplay SelectKey(KeyCode key, Color color)
        {
            var display = FindDisplay(key);

            if (display != null)
                display.ChangeColor(color);

            return display;
        }

        private void UpdateKeysUp(List<KeyCode> keys)
        {
            foreach (var key in keys)
            {
                DeselectKey(key);

                if (IsShift(key))
                    AddShift(-1);
            }
        }

        private void DeselectKey(KeyCode key)
        {
            var display = FindDisplay(key);

            if (display != null)
            {
                if (display != _highlightedDisplay)
                    display.ResetColor();
                else
                    display.ChangeColor(Color.yellow); 
            }
        }

        private bool IsSupportiveKeys(KeyCode key)
            => IsShift(key) || key == KeyCode.Tab || key == KeyCode.CapsLock
            || key == KeyCode.Return || key == KeyCode.Backspace;

        private bool IsShift(KeyCode key)
            => key == KeyCode.LeftShift || key == KeyCode.RightShift;

        private KeyDisplay FindDisplay(KeyCode key)
        {
            int displayIndex = _displays.FindIndex(d => d.Key == key);
            if (displayIndex < 0)
                return null;
            
            return _displays[displayIndex];
        }
        #endregion Private
    }
}