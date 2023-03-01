using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;
using Object = System.Object;

public enum JsonType
{
    JsonUtlity,
    LisJson
}

public class JsonManager : BaseSingleton<JsonManager>
{
    
    public void SaveData(Object data, string filename, JsonType jsonType = JsonType.LisJson)
    {
        string path = Application.persistentDataPath + "/" + filename + ".json";
        string jsonStr = "";
        switch(jsonType)
        {
            case JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LisJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(jsonType), jsonType, null);
        }
        File.WriteAllText(path,jsonStr);
    }

    public T LoadData<T>(string filename, JsonType jsonType = JsonType.LisJson) where T : new()
    {
        string path = Application.persistentDataPath + "/" + filename + ".json";
        if(!File.Exists(path))
            path = Application.persistentDataPath + "/" + filename + ".json";
        if(!File.Exists(path))
            return new T();
        
        string jsonStr = File.ReadAllText(path);
        T data = default(T);

        switch(jsonType)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LisJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(jsonType), jsonType, null);
        }
        return data;
    }
}
