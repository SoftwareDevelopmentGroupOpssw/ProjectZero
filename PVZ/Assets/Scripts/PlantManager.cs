using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    //³äµç±¦
    EnergyFlower,
    //ÑÌç³
    Yanfei,
    //ÄªÄÈ
    Mona,
    //Äý¹â
    Ningguang,
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
            case PlantType.Yanfei:
                return GameManager.instance.GameConfig.Yanfei;
            case PlantType.Mona:
                return GameManager.instance.GameConfig.Mona;
            case PlantType.Ningguang:
                return GameManager.instance.GameConfig.Ningguang;
            default:
                break;
        }
        return null;
    }
}
