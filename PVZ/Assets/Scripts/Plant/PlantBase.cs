using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物的父类
/// </summary>
public abstract class PlantBase : MonoBehaviour
{

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Grid currentGrid;
    /// <summary>
    /// 查找自身相关组件
    /// </summary>
    protected void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 创建时的初始化
    /// </summary>
    /// <param name="inGrid"></param>
    public void InitForCreate(bool inGrid)
    {

        // 获取组件
        Find();
        // 拖拽时不播放动画
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        }
    }

    /// <summary>
    /// 放置时的初始化
    /// </summary>
    public void InitForPlace(Grid grid)
    {
        currentGrid = grid;
        //将当前植物基类脚本赋值给网格
        currentGrid.CurrentPlantBase = this;
        //当前植物的位置是网格中心
        transform.position = grid.Position;
        // 恢复动画
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        FunForPlace();
    }

    /// <summary>
    /// 创建一个虚基类，用于不同植物放置时各自的函数调用
    /// </summary>
    protected virtual void FunForPlace() { }
}
