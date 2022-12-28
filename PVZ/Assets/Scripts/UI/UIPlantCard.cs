using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // 遮罩图片的img组件
    private Image maskImg;
    // 冷却时间，几秒可以放置一次植物
    public float CDTime;
    // 当前时间：用于冷却时间的计算
    private float currTimeForCd;
    // 是否可以放置植物
    private bool canPlace;
    // 是否需要放置植物
    private bool wantPlace;
    // 用来创建的植物
    private PlantBase plant;
    // 在网格中的植物，是透明的
    private PlantBase plantInGrid;
    // 当前卡片所对应的植物类型
    public PlantType cardPlantType;

    public bool CanPlace
    {
        get => canPlace;
        set
        {
            canPlace = value;
            if (!canPlace)
            {
                // 完全遮住，表示不可以控制
                maskImg.fillAmount = 1;
                // 开始冷却
                CDEnter();
            }
            else
            {
                maskImg.fillAmount = 0;
            }
        }
    }
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                // 获取预制体
                GameObject tmpPlant = PlantManager.instance.GetPlantForType(cardPlantType);
                // 开始实例化，获取PlantBase脚本组件
                plant = Instantiate(tmpPlant, Vector3.zero, Quaternion.identity, PlantManager.instance.transform).GetComponent<PlantBase>();
                // 不在网格中的植物，也就是拖拽的植物
                plant.InitForCreate(false);
            }
            else
            {
                if (plant != null)
                {
                    Destroy(plant.gameObject);
                    plant = null;
                }
            }
        }
    }

    private void Update()
    {
        // 如果需要放置植物，并且要放置的植物不能为空
        if (WantPlace && plant != null)
        {
            // 让植物跟随鼠标
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //每帧获得当前鼠标最近的网格
            Grid grid = GridManager.instance.GetGridByWorldPos(mousePoint);
            // 拖拽的植物实时跟着鼠标动
            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
            // 如果距离网格距离小于0.5，需要在网格上出现一个透明的植物
            if (grid.HavePlant == false && Vector2.Distance(mousePoint, grid.Position) < 0.5)
            {
                if (plantInGrid == null)
                {
                    //由于plant是个脚本，所以需要获得这个脚本对应的游戏对象，实例化后再获得脚本
                    plantInGrid = Instantiate(plant.gameObject, grid.Position,
                        Quaternion.identity, PlantManager.instance.transform).GetComponent<PlantBase>();
                    // 在网格中的拟创建的虚拟植物
                    plantInGrid.InitForCreate(true);
                }
                // 已经创建，只需要改变它的位置到其余网格中
                else
                {
                    plantInGrid.transform.position = grid.Position;
                }
                // 点击鼠标放置植物
                if (Input.GetMouseButtonDown(0))
                {
                    // 实现放置
                    plant.InitForPlace(grid);
                    // 然后将存储GameObject的plant清空，和已经放置的植物实际上没有关系了
                    plant = null;
                    // 将网格中的虚拟植物游戏对象销毁
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                    // 不需要种植了，那么状态改变销毁plant
                    WantPlace = false;
                    //种植植物后进入CD
                    CanPlace = false;
                }
            }
            //如果距离过远(地图外)则创建植物失败
            else
            {
                if (plantInGrid != null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
            }
        }

        //右键取消放置植物
        if(Input.GetMouseButtonDown(1))
        {
            if(plant != null)
                Destroy(plant.gameObject);
            if(plantInGrid != null)
                Destroy(plantInGrid.gameObject);
            plant = null;
            plantInGrid = null;
            WantPlace = false;
        }
    }
    private void CDEnter()
    {
        // 遮住后，开始计算冷却
        StartCoroutine(CalCD());
    }
    // 计算冷却时间
    IEnumerator CalCD()
    {
        float calCD = (1 / CDTime) * 0.1f; // 1s减少阴影的值 * 0.1s = 0.1s减少的阴影的值
        currTimeForCd = CDTime;
        while (currTimeForCd >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskImg.fillAmount -= calCD;
            currTimeForCd -= 0.1f;
        }
        // 冷却时间结束
        CanPlace = true;
    }

    private void Start()
    {
        maskImg = transform.Find("Mask").GetComponent<Image>();
        CanPlace = false;
    }
    //设置选中植物聚焦
    // 鼠标移入执行
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
    // 鼠标移出执行
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1f, 1f);
    }
    // 点击后放置植物
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlace) return;
        if (!WantPlace)
        {
            WantPlace = true;
        }
    }
}
