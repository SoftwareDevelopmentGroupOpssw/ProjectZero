
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grid
{
    // 坐标点
    public Vector2 Point;

    // 世界坐标
    public Vector2 Position;

    // 是否有植物，如果有则不能创建植物
    public bool HavePlant;

    private PlantBase currentPlantBase;

    // 构造函数
    public Grid(Vector2 point, Vector2 position, bool havePlant)
    {
        Point = point;
        Position = position;
        HavePlant = havePlant;
    }

    public PlantBase CurrentPlantBase
    {
        get => currentPlantBase;
        set
        {
            currentPlantBase = value;
            if (currentPlantBase == null)
            {
                HavePlant = false;
            }
            else
            {
                HavePlant = true;
            }
        }
    }

}

