using System.Linq;
using UnityEngine;

namespace CodeBase.Utility
{
    public static class KeyboardInputUtility
    {
        static readonly KeyCode[] _keyCodes =
            System.Enum.GetValues(typeof(KeyCode))
                .Cast<KeyCode>()
                .Where(k => ((int)k < (int)KeyCode.Mouse0))
                .ToArray();

        public static bool KeyDown(ref KeyCode key)
        {
            if (Input.anyKeyDown)
                foreach (KeyCode keyCode in _keyCodes)
                    if (Input.GetKeyDown(keyCode))
                    {
                        key = keyCode;
                        return true;
                    }

            return false;
        }

        public static bool KeyPressed(ref KeyCode key)
        {
            if (Input.anyKey)
                foreach (KeyCode keyCode in _keyCodes)
                    if (Input.GetKey(keyCode))
                    {
                        key = keyCode;
                        return true;
                    }

            return false;
        }

        public static bool KeyUp(ref KeyCode key)
        {
            foreach (KeyCode keyCode in _keyCodes)
                if (Input.GetKeyUp(keyCode))
                    key = keyCode;

            return key != KeyCode.None;
        }
    }
}