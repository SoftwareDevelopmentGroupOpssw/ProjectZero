using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // ����ͼƬ��img���
    private Image maskImg;
    // ��ȴʱ�䣬������Է���һ��ֲ��
    public float CDTime;
    // ��ǰʱ�䣺������ȴʱ��ļ���
    private float currTimeForCd;
    // �Ƿ���Է���ֲ��
    private bool canPlace;
    // �Ƿ���Ҫ����ֲ��
    private bool wantPlace;
    // ����������ֲ��
    private GameObject plant;
    // �������е�ֲ���͸����
    private GameObject plantInGrid;
    // ��ǰ��Ƭ����Ӧ��ֲ������
    public PlantType cardPlantType;

    public bool CanPlace
    {
        get => canPlace;
        set
        {
            canPlace = value;
            if (!canPlace)
            {
                // ��ȫ��ס����ʾ�����Կ���
                maskImg.fillAmount = 1;
                // ��ʼ��ȴ
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
                // ��ȡԤ����
                GameObject tmpPlant = PlantManager.instance.GetPlantForType(PlantType.EnergyFlower);
                // ��ʼʵ����
                plant = Instantiate(tmpPlant, Vector3.zero, Quaternion.identity, PlantManager.instance.transform);
                // ���������е�ֲ�Ҳ������ק��ֲ��
                plant.GetComponent<EnergyFlower>().InitForCreate(false);
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
        // �����Ҫ����ֲ�����Ҫ���õ�ֲ�ﲻ��Ϊ��
        if (WantPlace && plant != null)
        {
            // ��ֲ��������ǵ����
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ��ק��ֲ��ʵʱ������궯
            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
            // ��������������С��0.5����Ҫ�������ϳ���һ��͸����ֲ��
            if (Vector2.Distance(mousePoint, GridManager.instance.GetGridPointByMouse()) < 0.5)
            {
                if (plantInGrid == null)
                {
                    plantInGrid = Instantiate(plant, GridManager.instance.GetGridPointByMouse(),
                        Quaternion.identity, PlantManager.instance.transform);
                    // �������е��ⴴ��������ֲ��
                    plantInGrid.GetComponent<EnergyFlower>().InitForCreate(true);
                }
                // �����Ѿ�����������ֻ��Ҫ�ı�����λ�õ�����������
                else
                {
                    plantInGrid.transform.position = GridManager.instance.GetGridPointByMouse();
                }
                // ���������ֲ��
                if (Input.GetMouseButtonDown(0))
                {
                    // ��Ȼ�Ѿ��������ô����ק�е�ֲ������������
                    plant.transform.position = GridManager.instance.GetGridPointByMouse();
                    // ʵ�������ķ���
                    plant.GetComponent<EnergyFlower>().InitForPlace();
                    // Ȼ�󽫴洢̫����GameObject��plant��գ����Ѿ����õ�ֲ��ʵ����û�й�ϵ��
                    plant = null;
                    // �������е�����ֲ������
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                    // ����Ҫ��ֲ�ˣ���ô״̬�ı�����plant
                    WantPlace = false;
                    //��ֲֲ������CD
                    CanPlace = false;
                }
            }
            //��������Զ(��ͼ��)�򴴽�ֲ��ʧ��
            else
            {
                if (plantInGrid != null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
            }
        }

        //�Ҽ�ȡ������ֲ��
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
        // ��ס�󣬿�ʼ������ȴ
        StartCoroutine(CalCD());
    }
    // ������ȴʱ��
    IEnumerator CalCD()
    {
        float calCD = (1 / CDTime) * 0.1f; // 1s������Ӱ��ֵ * 0.1s = 0.1s���ٵ���Ӱ��ֵ
        currTimeForCd = CDTime;
        while (currTimeForCd >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskImg.fillAmount -= calCD;
            currTimeForCd -= 0.1f;
        }
        // ��ȴʱ�����
        CanPlace = true;
    }

    private void Start()
    {
        maskImg = transform.Find("Mask").GetComponent<Image>();
        CanPlace = false;
    }
    //����ѡ��ֲ��۽�
    // �������ִ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
    // ����Ƴ�ִ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1f, 1f);
    }
    // ��������ֲ��
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlace) return;
        if (!WantPlace)
        {
            WantPlace = true;
        }
    }
}
