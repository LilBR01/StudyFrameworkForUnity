using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            if(instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }

    public virtual void Init(){}

    //在脚本实例被加载时执行
    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
        }

        Init();

        DontDestroyOnLoad(this.gameObject);
    }
}
