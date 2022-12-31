using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        Run();
    }
    Monster monster;
    public void Run()
    {
        UIManager.Instance.ShowPanel<TitlePanel>("TitlePanel");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GameController.Instance.IsGameStarted)
        {
            if (GameController.Instance.IsPaused)
                GameController.Instance.Resume();
            else
                GameController.Instance.Pause();
            //Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Bloom.AddSeed(new Vector3(world.x, world.y, 0));
        }
    }
}
