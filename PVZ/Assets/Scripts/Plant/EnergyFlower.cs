using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFlower : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {

    }
    private void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
    public void InitForPlace()
    {
        // 恢复动画
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        InvokeRepeating("CreateEnergy", 3, 3);
    }
    // Update is called once per frame

    private void CreateEnergy()
    {
        Energy energy = GameObject.Instantiate<GameObject>(GameManager.instance.GameConfig.Energy, transform.position, Quaternion.identity,
            transform).GetComponent<Energy>();
        //能量跳跃动画
        energy.JumpAnimation();
    }
}
