using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PoolManager : Singleton<PoolManager>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary <string, PoolData>();
    
    private GameObject poolObj;
    public void GetObj(string name, UnityAction<GameObject> callback)
    {
        if(poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            callback(poolDic[name].GetObj());
        }
        else
        {
            ResourceManager.Instance.LoadAsyn<GameObject>(name, (o) => 
            {
                o.name = name;
                callback(o);
            });
        }
    }

    public void PushObj(string name, GameObject obj)
    {
        if(poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }

        if(poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
    }

    //切换场景时清空，防止引用占用内存
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
