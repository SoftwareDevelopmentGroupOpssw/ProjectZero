using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    // ��Ϸ����
    // ������Դ�˵�
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Tooltip("����")]
    public GameObject Energy;
    [Tooltip("��籦")]
    public GameObject EnergyFlower;
}
