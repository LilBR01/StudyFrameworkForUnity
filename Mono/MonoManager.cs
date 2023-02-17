using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.ComponentModel;

public class MonoManager : Singleton<MonoManager>
{
    private MonoController controller;
    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);
    }

    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }

    public Coroutine mStartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    
    public Coroutine mStartCoroutine(string methodName, [DefaultValue("null")]object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public Coroutine mStartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
}
