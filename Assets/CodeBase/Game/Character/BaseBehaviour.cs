using UnityEngine;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover))]
    public abstract class BaseBehaviour : MonoBehaviour
    {
        [field: SerializeField] public Transform Body { get; private set; }

        private CharacterMover _mover;

        #region Public
        public void Init(Vector2 initPos)
        {
            _mover.Init(initPos, Body);
            Body.gameObject.SetActive(true);
        }

        public virtual void NextPositionX(float xPos)
        {
            _mover.StartMoveToNextPointX(xPos);
        }

        public void Hide()
        {
            Body.gameObject.SetActive(false);
        }
        #endregion Public

        protected abstract void OnEndMoving();

        #region Unity Lifecycle
        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            Hide();
        }

        private void OnEnable()
        {
            _mover.EndMoving += OnEndMoving;
        }

        private void OnDisable()
        {
            _mover.EndMoving -= OnEndMoving;
        }
        #endregion Unity Lifecycle
    }
}