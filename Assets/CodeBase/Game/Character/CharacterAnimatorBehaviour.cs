using UnityEngine;

namespace CodeBase.Game.Character
{
    public class CharacterAnimatorBehaviour : MonoBehaviour
    {
        private readonly string _moveParameterName = "IsMoving";

        [SerializeField] private Animator _animator;

        public void SetMoveParameter(bool isMove)
            => _animator.SetBool(_moveParameterName, isMove);
    }
}