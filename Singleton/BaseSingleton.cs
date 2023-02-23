using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//不继承mono的单例类
public class BaseSingleton<T> where T : BaseSingleton<T>
{
    private static BaseSingleton<T> instance;
    public BaseSingleton<T> Instance
    {
        get
        {
            if(instance == null)
                instance = new BaseSingleton<T>();
            return instance;
        }
    }
}
