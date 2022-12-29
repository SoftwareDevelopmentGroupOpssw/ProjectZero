using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 普通僵尸的行动脚本
/// </summary>
public class CommonZombie : Monster, IDamageable
{
    /// <summary>
    /// 一个对应普通魔物的效应处理器
    /// </summary>
    private CommonMonsterHandler handler;
    
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private Collider2D colliders;
    private Animator animator;
   
    /// <summary>
    /// 是否还存活
    /// </summary>
    private bool isAlive = true;

    /// <summary>
    /// 掉下来的头，死亡的时候会启用这个物体
    /// </summary>
    public GameObject FallingHead;

    public override IEffectHandler Handler => handler;



    private void Start()
    {
        handler = new CommonMonsterHandler(Data);

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        colliders = GetComponent<Collider2D>();

        //随机产生一个走路姿势
        System.Random walkStyle = new System.Random();
        animator.SetInteger("WalkStyle", walkStyle.Next(1, 3));

    }



    /// <summary>
    /// 走路
    /// </summary>
    public void Walk()
    {
        rigid.velocity = Vector2.left * Data.Speed / 100;
        animator.speed = Data.Speed / 50f;
    }
    /// <summary>
    /// 被眩晕
    /// </summary>
    public void Stun()
    {
        rigid.velocity = Vector2.zero;
        animator.speed = 0;//动画停止
    }
    /// <summary>
    /// 设置受到元素附着时的特效(改变颜色)
    /// </summary>
    public void RefreshElementState()
    {
        Color GetColor(Elements element)
        {
            switch (element)
            {
                case Elements.Water:
                    return new Color(0, 0.5f, 1f);
                case Elements.Fire:
                    return new Color(1f, 0.25f, 0);
                case Elements.Ice:
                    return new Color(0, 1f, 1f);
                case Elements.Electric:
                    return new Color(0.75f, 0, 1f);
                //风和岩不会附着
                case Elements.Grass:
                    return Color.green;
            }
            return Color.black;
        }
        Elements[] elements = Data.GetAllElements();
        Color color;
        if (elements.Length == 0)//没有元素附着，恢复到正常颜色
            color = Color.white;
        else if (elements.Length == 1)//只有一个元素附着
            color = GetColor(elements[0]);
        else//有多个元素附着
        {
            color = GetColor(elements[0]);
            for(int i = 1; i < elements.Length; i++)
            {
                color = (color + GetColor(elements[i])) / 2;
            }
        }
        sprite.color = color;
    }
    /// <summary>
    /// 超出地图删除
    /// </summary>
    public void CheckLocation()
    {
        if (GameController.Instance.WorldToGrid(transform.position) == new Vector2(-1, -1))
            Destroy(gameObject);
    }

    public void Update()
    {
        //用普通魔物效果分析器来对Data里的所有效果进行分析
        handler.CheckEffect(Data.GetEffects());
        if (isAlive)
        {
            //查找身上有没有眩晕效果
            IEffect stunEffect = Data.GetEffects().Find((effect) => effect is StunEffect);
            if (stunEffect == null)
                Walk();
            else
                Stun();

            CheckLocation();
            RefreshElementState();
        }

        if (Data.Health <= 0 && isAlive)//只有活着的时候才能死亡
            StartCoroutine(Die());
    }
    /// <summary>
    /// 死亡时触发的逻辑
    /// </summary>
    /// <returns></returns>
    IEnumerator Die()
    {
        isAlive = false;
        rigid.velocity = Vector2.zero;
        colliders.enabled = false;//关闭碰撞盒，这样就不会阻挡子弹
        float secondsBeforeBodyDisappear = 2;
        animator.Play("Die");
        FallingHead.SetActive(true);
        FallingHead.GetComponent<SpriteRenderer>().color = sprite.color;//把掉的头设置成和自己一样的颜色
        yield return new WaitForSecondsRealtime(secondsBeforeBodyDisappear);
        Destroy(gameObject);
    }

    public IDamageReceiver GetReceiver()
    {
        return Data;
    }
}
