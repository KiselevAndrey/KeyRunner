using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.Visibility
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupController : MonoBehaviour
    {
        private const float _defaultFrameTime = 1f / 30f;

        [SerializeField] private bool _isShowOnStart;
        [SerializeField, Min(0)] private float _duration = 1;
        [SerializeField, Range(0, 1)] private float _hideFactor = 1;

        public event UnityAction Showing;
        public event UnityAction Showed;
        public event UnityAction Hided;

        private CanvasGroup _canvasGroup;

        private State _state = State.Unknown;
        private Coroutine _changeAlphaSmooth;
        private WaitForSecondsRealtime _changeTime = new WaitForSecondsRealtime(_defaultFrameTime);

        private enum State
        {
            Unknown,
            Showing,
            Shown,
            Hidding,
            Hidden
        }

        public bool IsShown => _state == State.Shown;
        public bool IsHidden => _state == State.Hidden;
        public bool IsUnknown => _state == State.Unknown;

        private float HiddenAlpha => 1 - _hideFactor;

        private void SetState(State newState)
        {
            _state = newState;
            if (IsShown)
            {
                Showed?.Invoke();
            }
            else if (IsHidden)
            {
                Hided?.Invoke();
            }
            else if (_state == State.Showing)
            {
                Showing?.Invoke();
            }
        }

        public void Show()
        {
            if (_state == State.Showing || _state == State.Shown)
                return;

            SetState(State.Showing);

            if (_changeAlphaSmooth != null)
            {
                StopCoroutine(_changeAlphaSmooth);
            }

            _canvasGroup.alpha = 1;
            EnableInteraction();
        }

        public void ShowAndDestroy()
        {
            Show();
            Destroy(this);
        }

        public void ShowSmooth()
        {
            if (_state == State.Showing || _state == State.Shown)
                return;

            SetState(State.Showing);

            _changeAlphaSmooth = StartCoroutine(ChangeAlphaSmooth(1, _duration, EnableInteraction));
        }

        public void Hide()
        {
            SetState(State.Hidding);

            if (_changeAlphaSmooth != null)
            {
                StopCoroutine(_changeAlphaSmooth);
            }

            DisableInteraction();
            _canvasGroup.alpha = HiddenAlpha;
            SetState(State.Hidden);
        }

        public void HideAndDestroy()
        {
            Hide();
            DestroyOnHidden();
        }

        public void HideSmooth()
        {
            SetState(State.Hidding);
            DisableInteraction();
            _changeAlphaSmooth = StartCoroutine(ChangeAlphaSmooth(HiddenAlpha, _duration, () =>
            {
                SetState(State.Hidden);
            }));
        }

        public void HideSmoothAndDestroy()
        {
            DisableInteraction();
            _changeAlphaSmooth = StartCoroutine(ChangeAlphaSmooth(HiddenAlpha, _duration, () =>
            {
                SetState(State.Hidden);
                DestroyOnHidden();
            }));
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            if (IsUnknown)
            {
                if (_isShowOnStart)
                    Show();
                else
                    Hide();
            }
        }

        private void EnableInteraction()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            SetState(State.Shown);
        }

        private void DisableInteraction()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        private IEnumerator ChangeAlphaSmooth(float endAlpha, float duration, UnityAction endAction = null)
        {
            if (duration > 0)
            {
                float elapsed = 0;
                float startAlpha = _canvasGroup.alpha;

                while (elapsed <= duration)
                {
                    _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Sqrt(elapsed / duration));

                    if (Time.timeScale == 0)
                    {
                        elapsed += _defaultFrameTime;
                        yield return _changeTime;
                    }
                    else
                    {
                        elapsed += Time.deltaTime;
                        yield return null;
                    }
                }
            }

            _canvasGroup.alpha = endAlpha;
            endAction?.Invoke();
        }

        private void DestroyOnHidden()
        {
            if (_hideFactor == 1)
                Destroy(gameObject);
            else
                Destroy(this);
        }
    }
}