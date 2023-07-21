using UnityEngine;

namespace CodeBase.UI.Gameplay
{
    public interface IGameplayKeyboardChecker
    {
        public bool IsRightKey(KeyCode key, bool isShiftPressed);
    }
}
