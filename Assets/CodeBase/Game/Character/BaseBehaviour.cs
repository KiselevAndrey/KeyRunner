using UnityEngine;

namespace CodeBase.Game.Character
{
    public abstract class BaseBehaviour : MonoBehaviour
    {
        [field: SerializeField] public Transform Body { get; private set; }

        protected IMover Mover;

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
        protected abstract void OnEndMoving();

        #region Unity Lifecycle
        private void Awake()
        {
            OnAwake();
            Hide();
        }

        private void OnEnable()
        {
            Mover.EndMoving += OnEndMoving;
        }

        private void OnDisable()
        {
            Mover.EndMoving -= OnEndMoving;
        }
        #endregion Unity Lifecycle
    }
}