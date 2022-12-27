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
    // ������������
    public void UpdateEnergyNum(int num)
    {
        energyNumText.text = num.ToString();
    }
    // ��ȡ���������ı�����������
    public Vector3 GetEnergyNumTextPos()
    {
        // transform.position�Ǹ��ṹ�壬�޸�pos.xʵ�����Ƕ�position�ĸ������޸ģ���������޸����Ҫtransform.position = pos;
        // ����õ���������UI�ı�����Ļ���֮꣬������Z�ᣬ�����������������ͬ�ӽ��µ���Ļ����
        return energyNumText.transform.position;
    }


}
