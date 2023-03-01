using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//为了不调用mono使用mono中的方法
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(updateEvent != null)
        {
            updateEvent();
        }
    }

    public void AddUpdateListener(UnityAction func)
    {
        updateEvent += func;
    }

    public void RemoveUpdateListener(UnityAction func)
    {
        updateEvent -= func;
    }
}
