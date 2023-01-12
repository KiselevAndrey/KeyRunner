using UnityEngine;

namespace CodeBase.Game.Character
{
    public abstract class BaseStateSwith : MonoBehaviour
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

        public void StopMoving()
        {
            Mover.Stop();
            OnStoptMoving();
        }

        public void Hide()
        {
            Body.gameObject.SetActive(false);
        }
        #endregion Public

        protected abstract void OnAwake();
        protected virtual void OnStartMoving() { }
        protected virtual void OnStoptMoving() { }
        protected abstract void OnEndMoving();

        #region Unity Lifecycle
        private void Awake()
        {
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