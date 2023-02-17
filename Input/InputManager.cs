using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private bool isStart = false;

    public InputManager()
    {
        MonoManager.Instance.AddUpdateListener(MyUpdate);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void MyUpdate()
    {
        if(!isStart)    return;
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
    }

    private void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            EventCenter.Instance.EventTrigger("KeyisDown", key);
        }
        if(Input.GetKeyUp(key))
        {
            EventCenter.Instance.EventTrigger("KeyisUp", key);
        }
    }
}
