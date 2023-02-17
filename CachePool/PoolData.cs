using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    public GameObject fatherObj;
    public List<GameObject> poolList;
    public PoolData(GameObject obj, GameObject PoolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = PoolObj.transform;

        poolList = new List<GameObject>(){};

        PushObj(obj);
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }

    public GameObject GetObj()
    {
        GameObject obj = null;

        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;

        return obj;
    }
}
