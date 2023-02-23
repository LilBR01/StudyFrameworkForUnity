using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAStartType
{
    Walk,
    Stop
}

public class AStart
{
    public int x;
    public int y;
    public EAStartType type;
    public float f;
    public float g;
    public float h;

    public AStart father;

    public AStart(int x, int y, EAStartType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
