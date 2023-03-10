using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 和元素有关的函数事件
/// 可以用一个元素类型去访问，
/// </summary>
public class ElementEvent
{
    private readonly static int ELEMENTS_COUNT = System.Enum.GetValues(typeof(Elements)).Length;
    
    private System.Action[] actions = new System.Action[ELEMENTS_COUNT];
    private System.Action all;
    /// <summary>
    /// 清除所有的监听
    /// </summary>
    public void Clear()
    {
        for(int i = 0; i< ELEMENTS_COUNT;i++)
            actions[i] = null;
    }

    /// <summary>
    /// 为所有的元素都添加监听
    /// </summary>
    /// <param name="action">添加的监听</param>
    public void AddAllListener(System.Action action)
    {
        all += action;
    }
    /// <summary>
    /// 为所有的元素都移除监听
    /// </summary>
    /// <param name="action">移除的监听</param>
    public void RemoveAllListener(System.Action action)
    {
        all -= action;
    }
    /// <summary>
    /// 触发所有的监听
    /// </summary>
    public void TriggerAll()
    {
        all?.Invoke();
    }
    /// <summary>
    /// 添加元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    /// <param name="action">操作</param>
    public void AddListener(Elements element, System.Action action) => actions[(int)element] += action;
    /// <summary>
    /// 移除元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    /// <param name="action">操作</param>
    public void RemoveListener(Elements element, System.Action action) => actions[(int)element] -= action;
    /// <summary>
    /// 触发元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    public void Trigger(Elements element) => actions[(int)element]?.Invoke();
}
/// <summary>
/// 和元素有关的事件
/// </summary>
/// <typeparam name="T">事件函数的参数类型</typeparam>
public class ElementEvent<T>
{
    public const int ELEMENTS_COUNT = 8;
    private System.Action<T>[] actions = new System.Action<T>[ELEMENTS_COUNT];
    private System.Action<T> all;

    /// <summary>
    /// 清除所有的监听
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < ELEMENTS_COUNT; i++)
            actions[i] = null;
    }

    /// <summary>
    /// 为所有的元素都添加监听
    /// </summary>
    /// <param name="action">添加的监听</param>
    public void AddAllListener(System.Action<T> action)
    {
        all += action;
    }
    /// <summary>
    /// 为所有的元素都移除监听
    /// </summary>
    /// <param name="action">移除的监听</param>
    public void RemoveAllListener(System.Action<T> action)
    {
        all -= action;
    }
    /// <summary>
    /// 触发所有的监听
    /// </summary>
    public void TriggerAll(T item)
    {
        all?.Invoke(item);
    }

    /// <summary>
    /// 添加元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    /// <param name="action">操作</param>
    public void AddListener(Elements element, System.Action<T> action) => actions[(int)element] += action;
    /// <summary>
    /// 移除元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    /// <param name="action">操作</param>
    public void RemoveListener(Elements element, System.Action<T> action) => actions[(int)element] -= action;
    /// <summary>
    /// 触发元素监听
    /// </summary>
    /// <param name="element">元素类型</param>
    /// <param name="item">触发物体</param>
    public void Trigger(Elements element, T item) => actions[(int)element]?.Invoke(item);
}
