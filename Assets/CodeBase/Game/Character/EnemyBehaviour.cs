using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyBehaviour : BaseBehaviour
    {
        [SerializeField, Range(1, 4)] private float _startDistance = 2f;
        [SerializeField, Range(0, 1)] private float _catchDistance = .6f;
        [SerializeField, Range(0, 1)] private float _endDistance = .5f;
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
            base.NextPositionX(xPos - _endDistance);
        }

        protected override void OnAwake()
        {
            Mover = GetComponent<EnemyMover>();
        }

        protected override void OnEndMoving()
        {
            if (_target.position.x - _catchDistance <= Body.position.x)
            {
                base.OnEndMoving();
                CharacterCaught?.Invoke(_damage);
            }
            else
                NextPositionX(_target.position.x);
        }
    }
}