using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _speed;

        private Transform _character;
        private Coroutine _moveCoroutine;

        public event UnityAction EndMoving;

        public void Init(Vector2 startPosition, Transform character)
        {
            _character = character;
            _character.position = startPosition;
            StartMoveToNextPointX(startPosition.x);
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
            var finish = _character.position;
            finish.x = x;

            do
            {
                _character.position = Vector3.MoveTowards(_character.position, finish, _speed * Time.deltaTime);
                yield return null;
            } 
            while (_character.position != finish);

            EndMoving?.Invoke();
        }
    }
}