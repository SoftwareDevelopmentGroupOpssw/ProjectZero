using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Text energyNumText;

    private void Awake()
    {
        instance = this;
        energyNumText = transform.Find("MainPanel/EnergyNumText").GetComponent<Text>();
    }
    private void Start()
    {

    }
    // 更新能量数字
    public void UpdateEnergyNum(int num)
    {
        energyNumText.text = num.ToString();
    }
    // 获取能量数量文本的世界坐标
    public Vector3 GetEnergyNumTextPos()
    {
        // transform.position是个结构体，修改pos.x实际上是对position的复制体修改，所以如果修改最后还要transform.position = pos;
        // 这里得到的是能量UI文本的屏幕坐标，之所以有Z轴，是用来区分摄像机不同视角下的屏幕坐标
        return energyNumText.transform.position;
    }


}
