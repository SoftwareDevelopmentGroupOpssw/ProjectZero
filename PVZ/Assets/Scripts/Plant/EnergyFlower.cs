using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFlower : PlantBase
{

    private void CreateEnergy()
    {
        Energy energy = GameObject.Instantiate<GameObject>(GameManager.instance.GameConfig.Energy, transform.position, Quaternion.identity,
            transform).GetComponent<Energy>();
        //能量跳跃动画
        energy.JumpAnimation();
    }

    /// <summary>
    /// 能量花function,每隔3s生成1个能量
    /// </summary>
    protected override void FunForPlace()
    {
        InvokeRepeating("CreateEnergy", 3, 3);
    }
}
