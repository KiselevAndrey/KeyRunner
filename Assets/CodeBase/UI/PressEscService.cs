using System;
using UnityEngine;

namespace CodeBase.UI
{
    public class PressEscService : MonoBehaviour
    {
        private Action EscAction;

        public void Init(Action escAction)
        {
            EscAction = escAction;
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
                EscAction.Invoke();
        }
    }
}