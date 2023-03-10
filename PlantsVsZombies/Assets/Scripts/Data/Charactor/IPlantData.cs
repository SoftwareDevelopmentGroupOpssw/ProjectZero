using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物接口
/// </summary>
public interface IPlantData:ICharactorData , IDamageReceiver
{
    /// <summary>
    /// 植物的名字
    /// 在放置一个新的植物的时候，会用到这个名字去寻找数据库中的新实例
    /// </summary>
    public string PlantName { get; }
    /// <summary>
    /// 植物的说明 当鼠标悬停在植物上时会弹出说明窗口
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// 植物的能量花费
    /// </summary>
    public int EnergyCost { get; }
    /// <summary>
    /// 卡槽图片
    /// </summary>
    public Sprite CardSprite { get; }
    /// <summary>
    /// 总冷却时间（毫秒）
    /// </summary>
    public int CoolTime { get; }
}
