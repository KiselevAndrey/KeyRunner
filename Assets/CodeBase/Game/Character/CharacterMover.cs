using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    public class CharacterMover : MonoBehaviour, IMover
    {
        [SerializeField, Min(0)] private float _speed;

        private Transform _body;
        private Coroutine _moveCoroutine;

        public event UnityAction StartMoving;
        public event UnityAction EndMoving;

        protected virtual float Speed => _speed;

        public void Init(Vector2 startPosition, Transform body)
        {
            _body = body;
            _body.position = startPosition;
            Stop();
        }

        public void StartMoveToNextPointX(float x)
        {
            Stop();
            _moveCoroutine = StartCoroutine(MovingTo(x));
        }

        public void Stop()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
        }

        private IEnumerator MovingTo(float x)
        {
            StartMoving?.Invoke();

            var finish = _body.position;
            finish.x = x;

            do
            {
                _body.position = Vector3.MoveTowards(_body.position, finish, Speed * Time.deltaTime);
                yield return null;
            } 
            while (_body.position != finish);

            EndMoving?.Invoke();
        }
    }
}