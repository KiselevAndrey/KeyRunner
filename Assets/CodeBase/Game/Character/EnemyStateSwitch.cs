using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyStateSwitch : BaseStateSwith
    {
        [SerializeField, Range(1, 4)] private float _startDistance = 2f;
        [SerializeField, Range(0, 1)] private float _catchDistance = .6f;
        [SerializeField, Range(0, 1)] private float _endDistance = .5f;
        [SerializeField, Min(0)] private int _damage = 10;

        private Transform _target;
        private EnemyHunting _huntingBehaviour;

        public event UnityAction CharacterCaught;
        public event UnityAction<int> HuntingEnding;

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
            _huntingBehaviour = GetComponent<EnemyHunting>();
        }

        protected override void OnEndMoving()
        {
            if (_target.position.x - _catchDistance <= Body.position.x)
            {
                CharacterCaught?.Invoke();
                _huntingBehaviour.StartHunting(EndHuntingAction);
            }
            else
                NextPositionX(_target.position.x);
        }

        private void EndHuntingAction()
            => HuntingEnding?.Invoke(_damage);
    }
}