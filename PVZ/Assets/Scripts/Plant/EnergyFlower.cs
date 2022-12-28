using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFlower : PlantBase
{

    private void CreateEnergy()
    {
        Energy energy = GameObject.Instantiate<GameObject>(GameManager.instance.GameConfig.Energy, transform.position, Quaternion.identity,
            transform).GetComponent<Energy>();
        //������Ծ����
        energy.JumpAnimation();
    }

    /// <summary>
    /// ������function,ÿ��3s����1������
    /// </summary>
    protected override void FunForPlace()
    {
        InvokeRepeating("CreateEnergy", 3, 3);
    }
}
