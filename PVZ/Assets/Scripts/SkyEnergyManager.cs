using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkyEnergyManager : MonoBehaviour
{
    //��������������y
    private float energyPosY = 6;
    //����������X�᷶Χ
    private float energyMaxPosX = 6.2F;
    private float energyMinPosX = -6.5F;
    //�������������y�᷶Χ
    private float energyDownMaxPos = 3.6F;
    private float energyDownMinPos = -2.2F;
    void Start()
    {
        
        //ÿ��10s����һ������
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
