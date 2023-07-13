using UnityEngine;

namespace CodeBase.Utility
{
    public class UnityLificycleListner : MonoBehaviour
    {
        [Tooltip("Listners")]
        [SerializeField] private bool _awake;
        [SerializeField] private bool _start;
        [SerializeField] private bool _enable;
        [SerializeField] private bool _disable;
        [SerializeField] private bool _destroy;

        private void Awake()
        {
            if (_awake)
                Debug.Log($"{transform} OnAwake");
        }

        private void Start()
        {
            if (_start)
                Debug.Log($"{transform} OnStart");
        }

        private void OnEnable()
        {
            if (_enable)
                Debug.Log($"{transform} OnEnable");

        }

        private void OnDisable()
        {
            if (_disable)
                Debug.Log($"{transform} OnDisable");

        }

        private void OnDestroy()
        {
            if (_destroy)
                Debug.Log($"{transform} OnDestroy");

        }
    }
}