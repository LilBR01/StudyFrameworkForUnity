using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarManager : Singleton<AstarManager>
{
    private int mapW;
    private int mapH;
    //地图所有格子对象容器
    public AstarNode[,] nodes;

    private List<AstarNode> openList = new List<AstarNode>();
    private List<AstarNode> closeList = new List<AstarNode>();

    //初始化地图信息
    //创建对应的格子
    public void InitMapInfo(int w, int h)
    {
        nodes = new AstarNode[w, h];
        
        for(int i = 0; i < w; ++i)
        {
            for(int j = 0; j < h; ++j)
            {
                //随机阻挡
                //项目中的阻挡信息应该是从配置文件读取的
                AstarNode node = new AstarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_Type.stop : E_Node_Type.walk);
                nodes[i,j] = node;
            }
        }
        //记录宽高
        mapW = w;
        mapH = h;
    }

    //寻路方法
    public List<AstarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {   
        //判断传进来的两个点是否合法
        //1.判断是否在地图范围内
        if(startPos.x < 0 || startPos.x >= mapW || startPos.y < 0 || startPos.y >= mapH || endPos.x < 0 || endPos.x >= mapW || endPos.y < 0 || endPos.y>= mapH)
            return null;

        //2.是不是阻挡
        AstarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AstarNode end = nodes[(int)endPos.x, (int)endPos.y];
        if(start.type == E_Node_Type.stop || end.type == E_Node_Type.stop)
            return null;
        
        //清空关闭和开启列表
        //防止上一次相关的数据影响这一次的寻路计算
        closeList.Clear();
        openList.Clear();
        
        //把开始点放入关闭列表中
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);

        while(true)
        {
            //从起点开始找周围的点，并将其放入开启列表中
            FindNearlyNodeToOpenList ((start.x-1), start.y-1, 1.4f, start, end);
            FindNearlyNodeToOpenList ((start.x), start.y-1, 1, start, end);
            FindNearlyNodeToOpenList ((start.x+1), start.y-1, 1.4f, start, end);
            FindNearlyNodeToOpenList ((start.x-1), start.y, 1, start, end);
            FindNearlyNodeToOpenList ((start.x+1), start.y, 1, start, end);
            FindNearlyNodeToOpenList ((start.x-1), start.y+1, 1.4f, start, end);
            FindNearlyNodeToOpenList ((start.x), start.y+1, 1, start, end);
            FindNearlyNodeToOpenList ((start.x+1), start.y+1, 1.4f, start, end);

            //死路判断
            if(openList.Count == 0)
            {
                return null;
            }

            //选出开启列表中，寻路消耗最小的点
            openList.Sort(SortOpenList);

            //放入关闭列表中，然后再从开启列表中移除
            closeList.Add(openList[0]);
            //找到的点是下一次起点，进行下一次寻路计算
            start = openList[0];
            openList.RemoveAt(0);

            //如果这个点是终点了，那么得到最终结果返回出去
            //如果这个点不是终点，那么继续寻路
            if(start == end)
            {
                //路径列表
                List<AstarNode> path = new List<AstarNode>();
                path.Add(end);
                while(end.father != null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                //列表反转
                path.Reverse();

                return path;
            }
        }
    }

    private int SortOpenList(AstarNode a, AstarNode b)
    {
        if (a.f > b.f)
            return 1;
        else if (a.f == b.f)
            return 1;
        else
            return -1;
    }

    private void FindNearlyNodeToOpenList(int x, int y, float g, AstarNode father, AstarNode end)
    {
        //边界判断
        if(x < 0 || x >= mapW || y < 0 || y >= mapH)
           return;

        AstarNode node = nodes[x, y];

        //判断是否是阻挡,并且如果在列表中了也不需要对其进行操作了
        if(node == null || node.type == E_Node_Type.stop || closeList.Contains(node) || openList.Contains(node))
            return;
        
        //计算f值 f = g + h
        node.father = father;
        node.g = father.g + g;
        //曼哈顿街区距离（十字）
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f= node.g + node.h;

        openList.Add(node);
    }
}