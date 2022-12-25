using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 游戏控制器
/// 当程序运行时作为物体的脚本自动被添加到场景中，只有一个实例
/// 当游戏进行中时为Active，游戏不在进行中时为Disactive
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController instance;
    /// <summary>
    /// 单例
    /// </summary>
    public static GameController Instance => instance;
    
    /// <summary>
    /// 关卡信息
    /// </summary>
    public ILevelData LevelData { get; set; }//当选择关卡完成时，设置关卡
    /// <summary>
    /// 装载着地图Sprite的游戏对象
    /// </summary>
    public GameObject Level { get; private set; }
    /// <summary>
    /// 所有植物的控制器
    /// </summary>
    private PlantsController plantsController;
    /// <summary>
    /// 所有飞行物的控制器
    /// </summary>
    private FlyersController flyerController;

    private MonstersController monsterController;
    /// <summary>
    /// 所有魔物的控制器
    /// </summary>
    public MonstersController MonstersController => monsterController;
    
    /// <summary>
    /// 已选择的植物
    /// </summary>
    private List<PlantsSelected> selected = new List<PlantsSelected>();

    /// <summary>
    /// 能量管理模块
    /// </summary>
    public EnergyMonitor Energy { get; private set; } = new EnergyMonitor();


    /// <summary>
    /// （给UI系统使用）
    /// 在指定位置处尝试放置一个植物
    /// </summary>
    /// <param name="selectIndex">选择的植物编号</param>
    /// <param name="pixelPos">指定位置的像素坐标</param>
    /// <returns>放置结果成功与否</returns>
    public bool TryPlacePlant(int selectIndex,Vector2Int pixelPos)
    {
        //TODO:实现逻辑判断
        return true;
    }
    
    /// <summary>
    /// （给UI系统使用）
    /// 在指定位置处尝试移除一个植物
    /// </summary>
    /// <param name="pixelPos">指定位置的像素位置</param>
    /// <returns>移除结果是否成功</returns>
    public bool TryRemovePlant(Vector2Int pixelPos)
    {
        //TODO:实现逻辑判断
        return true;
    }
    
    /// <summary>
    /// 添加飞行物
    /// </summary>
    /// <param name="data">飞行物数据</param>
    /// <param name="pixelPos">飞行物出现的像素坐标</param>
    /// <returns>飞行物对象</returns>
    public Flyer AddFlyer(IFlyerData data, Vector2Int pixelPos) => flyerController.AddFlyer(data, pixelPos);


    /// <summary>
    /// 游戏启动前相关逻辑
    /// </summary>
    void OnEnable()
    {
        
    }
    /// <summary>
    /// 游戏结束时相关逻辑
    /// </summary>
    void OnDisable()
    {

    }
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        plantsController = new PlantsController(LevelData);
        flyerController = new FlyersController();
        monsterController = new MonstersController(LevelData);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 游戏是否正在运行
    /// </summary>
    public bool IsGameStarted => gameObject.activeSelf;
    /// <summary>
    /// 启动游戏
    /// </summary>
    public void StartGame()
    {
        gameObject.SetActive(true);
        UIManager.Instance.ShowPanel<LevelBackground>
            (
                "LevelBackground",
                UIManager.UILayer.Top,
                (panel) =>
                {
                    panel.Sprite = LevelData.Sprite;
                    Level = panel.gameObject;
                }
            );
        UIManager.Instance.ShowPanel<PlantsCardPanel>("PlantsCardPanel",UIManager.UILayer.Mid,(panel)=>panel.SetPlotCount(8));

        for (int i = 200; i < 800; i += 100)
        {
            EnergyMonitor.InstantiateEnergy(new Vector2Int(i, 540), EnergyType.Big);
        }
    }
    /// <summary>
    /// 结束游戏
    /// </summary>
    public void EndGame()
    {
        gameObject.SetActive(false);
    }
}
