using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Utility
{
    public static class KeyboardInputUtility
    {
        static readonly KeyCode[] _keyCodes =
            System.Enum.GetValues(typeof(KeyCode))
                .Cast<KeyCode>()
                .Where(k => (int)k < (int)KeyCode.Mouse0)
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

        public static bool KeysInfo(out List<KeyCode> downedKeys, out List<KeyCode> pressedKeys, out List<KeyCode> uppenedKeys)
        {
            downedKeys = new List<KeyCode>();
            pressedKeys = new List<KeyCode>();
            uppenedKeys = new List<KeyCode>();

            foreach (KeyCode keyCode in _keyCodes)
            {
                if(Input.GetKeyDown(keyCode))
                    downedKeys.Add(keyCode);
                else if(Input.GetKey(keyCode))
                    pressedKeys.Add(keyCode);
                else if(Input.GetKeyUp(keyCode))
                    uppenedKeys.Add(keyCode);
            }

            return downedKeys.Count > 0 
                || pressedKeys.Count > 0 
                || uppenedKeys.Count > 0;
        }
    }
}