using UnityEngine.Events;

namespace CodeBase.Gameplay
{
    public interface IGameplayInformer
    {
        public event UnityAction<int> ScoreChanged;
        public event UnityAction EndGame;
    }
}
