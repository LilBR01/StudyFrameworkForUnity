using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_UI_Layer
{
    Bot,
    Mid,
    Top
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform bot;
    private Transform mid;
    private Transform top;


    public UIManager()
    {
        GameObject obj = ResourceManager.Instance.Load<GameObject>("UI/Canvas");

        Transform canvas = obj.transform;

        GameObject.DontDestroyOnLoad(obj);

        bot = canvas.Find("bot");
        mid = canvas.Find("mid");
        top = canvas.Find("top");

        obj = ResourceManager.Instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Top, UnityAction<T> callback = null) where T : BasePanel
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if(callback != null)
            {
                callback(panelDic[panelName] as T);
            }
            return;
        }

        ResourceManager.Instance.LoadAsyn<GameObject>("UI/Panel/"+panelName,(obj) => {
            Transform father = bot;
            switch(layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
            }

            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panel = obj.GetComponent<T>();

            if(callback != null)    callback(panel);

            panelDic.Add(panelName, panel);
        });
    }

    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }
}
