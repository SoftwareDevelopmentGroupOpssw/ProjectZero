
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grid
{
    // �����
    public Vector2 Point;

    // ��������
    public Vector2 Position;

    // �Ƿ���ֲ���������ܴ���ֲ��
    public bool HavePlant;

    private PlantBase currentPlantBase;

    // ���캯��
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

