using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover), typeof(CharacterAnimator))]
    public class CharacterStateSwitch : BaseStateSwith
    {
        private CharacterAnimator _animator;

        public Vector3 Position => Body.position;

        public event UnityAction EndMoving;

        protected override void OnAwake()
        {
            Mover = GetComponent<CharacterMover>();
            _animator = GetComponent<CharacterAnimator>();
        }

        protected override void OnStartMoving() 
            => _animator.SetMoveParameter(true);

        protected override void OnStoptMoving()
            => _animator.SetMoveParameter(false);

        protected override void OnEndMoving()
        {
            _animator.SetMoveParameter(false);
            EndMoving?.Invoke();
        }
    }
}