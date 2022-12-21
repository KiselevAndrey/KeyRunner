using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    public class CharacterMover : MonoBehaviour, IMover
    {
        [SerializeField, Min(0)] private float _speed;

        private Transform _character;
        private Coroutine _moveCoroutine;

        public event UnityAction StartMoving;
        public event UnityAction EndMoving;

        protected virtual float Speed => _speed;

        public void Init(Vector2 startPosition, Transform character)
        {
            _character = character;
            _character.position = startPosition;
            Stop();
            //StartMoveToNextPointX(startPosition.x);
        }

        public void StartMoveToNextPointX(float x)
        {
            Stop();
            _moveCoroutine = StartCoroutine(MovingTo(x));
        }

        private void Stop()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
        }

        private IEnumerator MovingTo(float x)
        {
            StartMoving?.Invoke();

            var finish = _character.position;
            finish.x = x;

            do
            {
                _character.position = Vector3.MoveTowards(_character.position, finish, Speed * Time.deltaTime);
                yield return null;
            } 
            while (_character.position != finish);

            EndMoving?.Invoke();
        }
    }
}