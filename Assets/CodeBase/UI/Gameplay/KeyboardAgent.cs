using CodeBase.UI.Keyboard;
using UnityEngine;

namespace CodeBase.UI.Gameplay
{
    [System.Serializable]
    public class KeyboardAgent
    {
        [SerializeField] private KeyboardService _keyboard;

        private IGameplayKeyboardChecker _checker;

        public void Init(IGameplayKeyboardChecker checker)
        {
            _checker = checker;
        }

        public void OnEnable()
        {
            _keyboard.PressedKey += OnKeyPressed;
        }

        public void OnDisable()
        {
            _keyboard.PressedKey -= OnKeyPressed;
        }

        private void OnKeyPressed(KeyCode key, bool isShiftPressed)
        {
            _checker.IsRightKey(key, isShiftPressed);
        }
    }
}
