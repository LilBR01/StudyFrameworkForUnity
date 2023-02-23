using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        FindChildControl<Button>();
        FindChildControl<Image>();
        FindChildControl<Text>();
        FindChildControl<Toggle>();
        FindChildControl<Scrollbar>();
        FindChildControl<Slider>();
    }

    //得到对应名字的对应控件脚本
    protected T GetControl<T>(string controlName) where T: UIBehaviour
    {
        if(controlDic.ContainsKey(controlName))
        {
            for(int i = 0; i < controlDic[controlName].Count; i++)
            {
                if(controlDic[controlName][i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }
        return null;
    }

    //找到相对应的UI控件并记录到字典中
    private void FindChildControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        string objName;
        for(int i = 0; i < controls.Length; i++)
        {
            objName = controls[i].gameObject.name;
            if(controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>(){controls[i]});
            }
        }
    }

    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }
}
