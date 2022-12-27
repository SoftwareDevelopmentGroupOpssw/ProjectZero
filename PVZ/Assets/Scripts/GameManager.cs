using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameConfig GameConfig { get; private set; }

    public static GameManager instance;
    // ����������
    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        GameConfig = Resources.Load<GameConfig>("GameConfig");
    }
}
