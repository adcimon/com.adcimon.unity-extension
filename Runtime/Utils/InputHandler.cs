using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public enum KeyState { Down, Pressed, Up }
    public enum AxisState { Normal, Raw }
    public enum Comparison { Less, LessEqual, Equal, GreaterEqual, Greater }

    [Serializable] public class OnKeyEvent : UnityEvent<KeyCode, KeyState> { }
    [Serializable] public class OnAxisEvent : UnityEvent<string, float> { }

    [Serializable]
    public class KeyInput
    {
        public string name;
        public KeyCode code;
        public KeyState state;
        public OnKeyEvent onEvent;
    }

    [Serializable]
    public class AxisInput
    {
        public string name;
        public string code;
        public AxisState state;
        public OnAxisEvent onEvent;
        public List<AxisComparison> comparisons = new List<AxisComparison>();
    }

    [Serializable]
    public class AxisComparison
    {
        public float value;
        public Comparison comparison = Comparison.Equal;
    }

    public List<KeyInput> keyInputs = new List<KeyInput>();
    public List<AxisInput> axisInputs = new List<AxisInput>();

    private void Update()
    {
        HandleKeys();
        HandleAxes();
    }

    private void HandleKeys()
    {
        for( int i = 0; i < keyInputs.Count; i++ )
        {
            KeyInput key = keyInputs[i];

            switch( key.state )
            {
                case KeyState.Down:
                {
                    if( Input.GetKeyDown(key.code) )
                    {
                        key.onEvent.Invoke(key.code, key.state);
                    }

                    break;
                }
                case KeyState.Pressed:
                {
                    if( Input.GetKey(key.code) )
                    {
                        key.onEvent.Invoke(key.code, key.state);
                    }

                    break;
                }
                case KeyState.Up:
                {
                    if( Input.GetKeyUp(key.code) )
                    {
                        key.onEvent.Invoke(key.code, key.state);
                    }

                    break;
                }
            }
        }
    }

    private void HandleAxes()
    {
        for( int i = 0; i < axisInputs.Count; i++ )
        {
            AxisInput axis = axisInputs[i];

            float value = (axis.state == AxisState.Normal) ? Input.GetAxis(axis.code) : (Input.GetAxisRaw(axis.code));

            bool flag = (axis.comparisons.Count > 0);
            for( int j = 0; j < axis.comparisons.Count; j++ )
            {
                if( !flag )
                {
                    break;
                }

                AxisComparison comparison = axis.comparisons[j];
                switch( comparison.comparison )
                {
                    case Comparison.Less:
                    {
                        flag = flag && (value < comparison.value);
                        break;
                    }
                    case Comparison.LessEqual:
                    {
                        flag = flag && (value <= comparison.value);
                        break;
                    }
                    case Comparison.Equal:
                    {
                        flag = flag && (value == comparison.value);
                        break;
                    }
                    case Comparison.GreaterEqual:
                    {
                        flag = flag && (value >= comparison.value);
                        break;
                    }
                    case Comparison.Greater:
                    {
                        flag = flag && (value > comparison.value);
                        break;
                    }
                }
            }

            if( flag )
            {
                axis.onEvent.Invoke(axis.code, value);
            }
        }
    }
}