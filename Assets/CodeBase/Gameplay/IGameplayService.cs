using UnityEngine;

namespace CodeBase.Gameplay
{
    public interface IGameplayService
    {
        public bool IsRightKey(KeyCode key, bool isShiftPressed);
    }
}
