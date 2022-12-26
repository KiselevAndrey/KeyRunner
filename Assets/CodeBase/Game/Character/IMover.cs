using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    public interface IMover
    {
        public event UnityAction StartMoving;
        public event UnityAction EndMoving;

        public void Init(Vector2 startPosition, Transform character);
        public void StartMoveToNextPointX(float x);
        public void Stop();
    }
}