using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 飞行物脚本基类
/// </summary>
public abstract class Flyer : MonoBehaviour
{
    /// <summary>
    /// 飞行物数据
    /// </summary>
    public IFlyerData Data { get; set; }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Data.OnAwake();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Data.OnTriggered(collision.gameObject);
    }
    protected virtual void OnDestroy()
    {
        Data.OnDestroy();
    }
}
