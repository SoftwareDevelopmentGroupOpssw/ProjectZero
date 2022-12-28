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

    private void CreateGridsBaseGrid()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                // ���ڸýű���������Ϸ�������ڸ�Ŀ¼������transform.position����������
                GridList.Add(new Grid(new Vector2(i, j),
                    transform.position + new Vector3((float)(0.1 + 1.35f * i), (float)(0.25 + 1.35f * j)), false));
            }
        }
    }

    /// <summary>
    /// ͨ������ȡ������������������
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Vector2 GetGridPointByMouse(Vector2 worldPos)
    {
        return GetGridByWorldPos(worldPos).Position;
    }

    /// <summary>
    /// ͨ�����������ȡ��������������
    /// </summary>
    /// <returns></returns>
    public Grid GetGridByWorldPos(Vector2 worldPos)
    {
        float dis = 999999;
        Grid grid = null;
        for (int i = 0; i < GridList.Count; ++i)
        {
            float mouseToGrid = Vector2.Distance(worldPos, GridList[i].Position);
            if (mouseToGrid < dis)
            {
                dis = mouseToGrid;
                grid = GridList[i];
            }
        }
        return grid;
    }
}
