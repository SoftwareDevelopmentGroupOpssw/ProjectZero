using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    private List<Vector2> pointList = new List<Vector2>();

    private List<Grid> GridList = new List<Grid>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateGridsBaseGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(GetGridPointByMouse());
        }
    }

    private void CreateGridsBaseGrid()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                // 由于该脚本依附的游戏对象是在根目录，所以transform.position是世界坐标
                GridList.Add(new Grid(new Vector2(i, j),
                    transform.position + new Vector3((float)(0.1 + 1.35f * i), (float)(0.25 + 1.35f * j)), false));
            }
        }
    }

    public Vector2 GetGridPointByMouse()
    {

        float dis = 999999;
        Vector2 point = new Vector2();
        for (int i = 0; i < GridList.Count; ++i)
        {
            float mouseToGrid = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position);
            if (mouseToGrid < dis)
            {
                dis = mouseToGrid;
                point = GridList[i].Position;
            }
        }
        return point;
    }
}
