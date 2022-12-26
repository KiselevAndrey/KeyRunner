using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover), typeof(CharacterAnimatorBehaviour))]
    public class CharacterBehaviour : BaseBehaviour
    {
        private CharacterAnimatorBehaviour _animator;

        public Vector3 Position => Body.position;

        public event UnityAction EndMoving;

        public void StopMoving()
        {
            Mover.Stop();
            OnEndMoving();
        }

        protected override void OnAwake()
        {
            Mover = GetComponent<CharacterMover>();
            _animator = GetComponent<CharacterAnimatorBehaviour>();
        }

        protected override void OnStartMoving() 
            => _animator.SetMoveParameter(true);

        protected override void OnEndMoving()
        {
            _animator.SetMoveParameter(false);
            EndMoving?.Invoke();
        }
    }
}