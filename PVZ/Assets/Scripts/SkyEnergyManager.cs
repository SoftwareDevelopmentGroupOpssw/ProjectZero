using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkyEnergyManager : MonoBehaviour
{
    //创建能量的坐标y
    private float energyPosY = 6;
    //创建能量的X轴范围
    private float energyMaxPosX = 6.2F;
    private float energyMinPosX = -6.5F;
    //创建能量掉落的y轴范围
    private float energyDownMaxPos = 3.6F;
    private float energyDownMinPos = -2.2F;
    void Start()
    {
        
        //每隔10s生成一个能量
        InvokeRepeating("CreateEnergy", 3, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateEnergy()
    {
        Energy energy = GameObject.Instantiate(GameManager.instance.GameConfig.Energy, Vector3.zero, Quaternion.identity).GetComponent<Energy>();
        float downY = UnityEngine.Random.Range(energyDownMinPos, energyDownMaxPos);
        float createX = UnityEngine.Random.Range(energyMinPosX, energyMaxPosX);
        energy.Init(downY,createX, energyPosY);
    }
}
