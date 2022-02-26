using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    protected Vector3 targetPos;
    protected Vector3 startPos;
    protected MainCS main;
    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        startPos = transform.position;
        targetPos = transform.position + transform.forward * 2;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
    }

    protected void OpenDoor()
    {
        if (main.TestDistance(startPos, 3))//人物和门的起始位置 小于3
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.08f);//开门
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.08f);//关门
        }
    }
}
