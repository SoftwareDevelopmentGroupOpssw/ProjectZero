using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    // 阳光的数量
    private int energyNum;

    private void Awake()
    {
        instance = this;
    }
    public int EnergyNum
    {
        get => energyNum;
        set
        {
            energyNum = value;
            UIManager.instance.UpdateEnergyNum(energyNum);
        }
    }
    private void Start()
    {
        EnergyNum = 0;
    }
}
