using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStartMgr
{
    private static AStartMgr instance;

    public static AStartMgr Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new AStartMgr();
            }
            return instance;
        }
    }

    private int mapW;
    private int mapH;

    public AStart[,] nodes;

    private List<AStart> openList = new List<AStart>();

    private List<AStart> closeList = new List<AStart>();

    public void InitMapInfo(int w,int h)
    {
        nodes = new AStart[w, h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                AStart node = new AStart(i, j, Random.Range(0, 100) < 20 ? EAStartType.Stop : EAStartType.Walk);
                nodes[i, j] = node;
            }
        }
        mapW = w;
        mapH = h;

    }
    /// <summary>
    /// 找附近点，放入openlist
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="g"></param>
    /// <param name="father"></param>
    /// <param name="end"></param>
    private void FindNearlyNodeToOpenList(int x,int y,float g,AStart father,AStart end)
    {
        //判断边界
        if (x < 0 || x >= mapW || y < 0 || y >= mapH )
            return;

        AStart node = nodes[x, y];

        //判读是否边界，阻挡，开启，关闭
        if(node==null || 
            node.type==EAStartType.Stop||
            closeList.Contains(node)||
            openList.Contains(node))
            return;

        //计算f=g+h；
        node.father = father;
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;

        openList.Add(node);

    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int SortOpenList(AStart a,AStart b)
    {
        if (a.f > b.f)
            return 1;
        else
            return -1;

    }

    public List<AStart> FindPath(Vector2 startPos, Vector2 endPos)
    {
        //实际项目，传入的点，往往是坐标系中的位置

        //1、首先判断传入的两个点是否合法，也就是说是不是在地图范围内

        if (startPos.x < 0 || startPos.x >= mapW || startPos.y < 0 || startPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW || endPos.y < 0 || endPos.y >= mapH)
            return null;

        //2、判断是不是阻挡
        AStart start = nodes[(int)startPos.x, (int)startPos.y];
        AStart end = nodes[(int)endPos.x, (int)endPos.y];

        if (start.type==EAStartType.Stop || end.type==EAStartType.Stop)
            return null;

        //清空开启关闭列表
        openList.Clear();
        closeList.Clear();

        //3、把起点放入closeList中
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);
        while(true)
        {
            //从起点开始，找周围的点，放入开启列表
            FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);

            FindNearlyNodeToOpenList(start.x, start.y - 1, 1f, start, end);

            FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);

            FindNearlyNodeToOpenList(start.x - 1, start.y, 1f, start, end);

            FindNearlyNodeToOpenList(start.x + 1, start.y, 1f, start, end);

            FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);

            FindNearlyNodeToOpenList(start.x, start.y + 1, 1f, start, end);

            FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);

            //openlist为空，还没有找到重点
            if (openList.Count==0)
            {
                return null;
            }

            //选出开启列表，f最小的点
            openList.Sort(SortOpenList);
            //放入关闭列表
            closeList.Add(openList[0]);
            //以start为中心
            start = openList[0];
            openList.RemoveAt(0);

            //判断是否找到
            if (start == end)
            {
                List<AStart> path = new List<AStart>();
                path.Add(end);
                while(end.father!=null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                path.Reverse();
                return path;
            }

        }
    }
}
