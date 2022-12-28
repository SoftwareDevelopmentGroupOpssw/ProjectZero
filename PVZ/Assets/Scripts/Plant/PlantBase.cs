using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ��ĸ���
/// </summary>
public abstract class PlantBase : MonoBehaviour
{

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Grid currentGrid;
    /// <summary>
    /// ��������������
    /// </summary>
    protected void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ����ʱ�ĳ�ʼ��
    /// </summary>
    /// <param name="inGrid"></param>
    public void InitForCreate(bool inGrid)
    {

        // ��ȡ���
        Find();
        // ��קʱ�����Ŷ���
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        }
    }

    /// <summary>
    /// ����ʱ�ĳ�ʼ��
    /// </summary>
    public void InitForPlace(Grid grid)
    {
        currentGrid = grid;
        //����ǰֲ�����ű���ֵ������
        currentGrid.CurrentPlantBase = this;
        //��ǰֲ���λ������������
        transform.position = grid.Position;
        // �ָ�����
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        FunForPlace();
    }

    /// <summary>
    /// ����һ������࣬���ڲ�ֲͬ�����ʱ���Եĺ�������
    /// </summary>
    protected virtual void FunForPlace() { }
}
