using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ɰ��
/// </summary>
public class Sucrose : PeashooterBehaviour
{
    private Animator animator;//����״̬��
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// ��������
    /// </summary>
    void Atk()
    {
        animator.SetTrigger("Atk");//������������
    }

    protected override void Update()
    {
        base.Update();
        if (CountDown.Available && HaveMonster())
        {
            Atk();
            CountDown.StartCountDown();
        }
    }
}