using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    EnergyFlower
}
public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;
    private void Awake()
    {
        instance = this;
    }
    
    public GameObject GetPlantForType(PlantType type)
    {
        switch(type)
        {
            case PlantType.EnergyFlower:
                return GameManager.instance.GameConfig.EnergyFlower;
            default:
                break;
        }
        return null;
    }
}
