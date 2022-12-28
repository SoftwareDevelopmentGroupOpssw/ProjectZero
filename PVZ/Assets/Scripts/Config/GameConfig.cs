using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    // 游戏配置
    // 创建资源菜单
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Tooltip("能量")]
    public GameObject Energy;
    [Tooltip("充电宝")]
    public GameObject EnergyFlower;
    [Tooltip("烟绯")]
    public GameObject Yanfei;
    [Tooltip("莫娜")]
    public GameObject Mona;
    [Tooltip("凝光")]
    public GameObject Ningguang;
}
