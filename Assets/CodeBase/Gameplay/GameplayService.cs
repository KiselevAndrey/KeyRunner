using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Gameplay
{
    public class GameplayService : MonoBehaviour, IGameplayService, IGameplayInformer
    {
        public event UnityAction<int> ScoreChanged;
        public event UnityAction EndGame;

        public bool IsRightKey(KeyCode key, bool isShiftPressed)
        {
            Debug.Log($"{key}, isShiftPressed: {isShiftPressed}");

            return false;
        }
    }
}
