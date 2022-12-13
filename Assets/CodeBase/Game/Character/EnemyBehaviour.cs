using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover))]
    public class EnemyBehaviour : BaseBehaviour
    {
        [SerializeField, Min(0)] private float _startDistance = 2f;
        [SerializeField, Min(0)] private float _catchDistance = .5f;
        [SerializeField, Min(0)] private int _damage = 10;

        private Transform _target;

        public event UnityAction<int> CharacterCaught;

        public void Init(Vector2 initPos, Transform characterBody)
        {
            initPos.x -= _startDistance;
            Init(initPos);
            _target = characterBody;
        }

        public override void NextPositionX(float xPos)
        {
            base.NextPositionX(xPos - _catchDistance);
        }

        protected override void OnEndMoving()
        {
            if (_target.position.x - _catchDistance <= Body.position.x)
                CharacterCaught?.Invoke(_damage);
            else
                NextPositionX(_target.position.x);
        }
    }
}