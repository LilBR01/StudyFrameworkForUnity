using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    /// <summary>
    /// 左上角第一个cube的位置
    /// </summary>
    public int beginX = -3;
    public int beginY = 5;
    /// <summary>
    /// 每个cube的偏移量
    /// </summary>
    public int offsetX = 2;
    public int offsetY = -2;
    /// <summary>
    /// 地图宽高
    /// </summary>
    public int mapW = 5;
    public int mapH = 5;

    private Vector2 beginPos = Vector2.right * -1;

    public List<AStart> path = new List<AStart>();

    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        AStartMgr.Instance.InitMapInfo(mapW, mapH);
        for (int i = 0; i < mapW; ++i)
        {
            for (int j = 0; j < mapH; ++j)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                obj.name = i + "_" + j;
                cubes.Add(obj.name, obj);
                AStart node = AStartMgr.Instance.nodes[i, j];
                if (node.type==EAStartType.Stop)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,1000))
            {
                //記錄開始點，才能知道是第幾行
                
                //如果没有开始点;
                if (beginPos==Vector2.right*-1)
                {
                    //清理上一次路径
                    if (path != null)
                    {
                        for (int i = 0; i < path.Count; i++)
                        {
                            cubes[path[i].x + "_" + path[i].y].GetComponent<MeshRenderer>().material.color = Color.white;
                        }

                    }

                    string[] strs = hit.collider.gameObject.name.Split('_');
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else
                {
                    string[] strs = hit.collider.gameObject.name.Split('_');
                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    //避免死路不清除颜色
                    cubes[beginPos.x + "_" + beginPos.y].GetComponent<MeshRenderer>().material.color = Color.white;

                    path = AStartMgr.Instance.FindPath(beginPos, endPos);
                    if(path!=null)
                    {
                        for (int i = 0; i < path.Count; i++)
                        {
                            cubes[path[i].x + "_" + path[i].y].GetComponent<MeshRenderer>().material.color = Color.green;
                        }
                        
                    }                
                    beginPos = Vector2.right * -1;
                }

            }
        }
    }

}