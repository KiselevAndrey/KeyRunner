using UnityEngine;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterAnimatorBehaviour))]
    public abstract class BaseBehaviour : MonoBehaviour
    {
        [field: SerializeField] public Transform Body { get; private set; }

        protected IMover Mover;

        private CharacterAnimatorBehaviour _animator;

        #region Public
        public void Init(Vector2 initPos)
        {
            Mover.Init(initPos, Body);
            Body.gameObject.SetActive(true);
        }

        public virtual void NextPositionX(float xPos)
        {
            if (Body.gameObject.activeSelf)
                Mover.StartMoveToNextPointX(xPos);
        }

        public void Hide()
        {
            Body.gameObject.SetActive(false);
        }
        #endregion Public

        protected abstract void OnAwake();

        protected virtual void OnStartMoving()
            => _animator.SetMoveParameter(true);
        protected virtual void OnEndMoving()
            => _animator.SetMoveParameter(false);

        #region Unity Lifecycle
        private void Awake()
        {
            _animator = GetComponent<CharacterAnimatorBehaviour>();

            OnAwake();
            Hide();
        }

        private void OnEnable()
        {
            Mover.StartMoving += OnStartMoving;
            Mover.EndMoving += OnEndMoving;
        }

        private void OnDisable()
        {
            Mover.StartMoving -= OnStartMoving;
            Mover.EndMoving -= OnEndMoving;
        }
        #endregion Unity Lifecycle
    }
}