using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // 下落的目标点Y
    private float downTargetPosY;
    // 是否来自天空
    private bool isFromSky;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFromSky) return;
        if (transform.position.y <= downTargetPosY)
        {
            //能量自动销毁
            Invoke("DestroyEnergy", 5);
            return;
        }
        transform.Translate(Vector3.down * Time.deltaTime);
    }
    public void Init(float downTargetPosY, float createPosX, float createPosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector3(createPosX, createPosY, 0);
        isFromSky = true;
    }
    private void DestroyEnergy()
    {
        Destroy(gameObject);
    }

    // 鼠标点击原石时触发增加能量数量
    private void OnMouseDown()
    {
        PlayerManager.instance.EnergyNum += 25;
        // 将屏幕坐标转化为世界坐标
        Vector3 sunNumPos = Camera.main.ScreenToWorldPoint(UIManager.instance.GetEnergyNumTextPos());
        sunNumPos = new Vector3(sunNumPos.x, sunNumPos.y, 0);
        FlyAnimation(sunNumPos);
    }

    //跳跃动画，使用协程
    public void JumpAnimation()
    {
        isFromSky = false;
        StartCoroutine(DoJump());
        // 跳跃动作完后5s没有被点击则销毁
        Invoke("DestroySun", 5);
    }
    private IEnumerator DoJump()
    {
        // 随机获得是向左跳还是向右跳
        bool isLeft = Random.Range(0, 2) == 0;
        // 获取当前太阳花Y轴的大小
        Vector3 startPos = transform.position;
        float x;
        if (isLeft)
        {
            // 往左，就把方向向量x设为负数
            x = -0.035f;
        }
        else
        {
            // 往右，就把方向向量x设为负数
            x = 0.035f;
        }
        // 将高度设置为当前y上方1.5，在往左上方或者右上方移动时，判断是否超过，超过y就设置为负数，即开始掉落
        while (transform.position.y <= startPos.y + 1)
        {
            yield return new WaitForSeconds(0.005f);
            transform.Translate(new Vector3(x, 0.05f, 0));
        }
        while (transform.position.y >= startPos.y)
        {
            yield return new WaitForSeconds(0.005f);
            transform.Translate(new Vector3(x, -0.05f, 0));
        }
    }

    //阳光收集动画，使用协程
    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }
    private IEnumerator DoFly(Vector3 pos)
    {
        // 获得能量到能量文本的方向向量
        Vector3 direction = (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) > 0.5f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction); // 往这个方向移动
        }
        DestroyEnergy();
    }

}
