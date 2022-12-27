using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // �����Ŀ���Y
    private float downTargetPosY;
    // �Ƿ��������
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
            //�����Զ�����
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

    // �����ԭʯʱ����������������
    private void OnMouseDown()
    {
        PlayerManager.instance.EnergyNum += 25;
        // ����Ļ����ת��Ϊ��������
        Vector3 sunNumPos = Camera.main.ScreenToWorldPoint(UIManager.instance.GetEnergyNumTextPos());
        sunNumPos = new Vector3(sunNumPos.x, sunNumPos.y, 0);
        FlyAnimation(sunNumPos);
    }

    //��Ծ������ʹ��Э��
    public void JumpAnimation()
    {
        isFromSky = false;
        StartCoroutine(DoJump());
        // ��Ծ�������5sû�б����������
        Invoke("DestroySun", 5);
    }
    private IEnumerator DoJump()
    {
        // ������������������������
        bool isLeft = Random.Range(0, 2) == 0;
        // ��ȡ��ǰ̫����Y��Ĵ�С
        Vector3 startPos = transform.position;
        float x;
        if (isLeft)
        {
            // ���󣬾Ͱѷ�������x��Ϊ����
            x = -0.035f;
        }
        else
        {
            // ���ң��Ͱѷ�������x��Ϊ����
            x = 0.035f;
        }
        // ���߶�����Ϊ��ǰy�Ϸ�1.5���������Ϸ��������Ϸ��ƶ�ʱ���ж��Ƿ񳬹�������y������Ϊ����������ʼ����
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

    //�����ռ�������ʹ��Э��
    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }
    private IEnumerator DoFly(Vector3 pos)
    {
        // ��������������ı��ķ�������
        Vector3 direction = (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) > 0.5f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction); // ����������ƶ�
        }
        DestroyEnergy();
    }

}
