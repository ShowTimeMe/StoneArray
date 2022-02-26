using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bead : Machine
{
    protected int line;//行
    protected int num;//列
    protected Vector3 startPos;//开始位置
    protected Vector3 targetPos;//终止位置

    public bool isClicked;
    protected BeadGroup beadGroup;

    private void Start()
    {
        base.Start();
        beadGroup = BeadGroup.instance;
        SetStartValue(-transform.forward * 0.4f);
    }
    protected void SetStartValue(Vector3 v3)//初始化
    {
        line = int.Parse(name[4].ToString());//强转
        num=int.Parse(name[5].ToString())-1;
        startPos = transform.position;
        targetPos = startPos + v3;
    }
    protected override void OpenMachine()
    {
        if (beadGroup.doorIsOpen)//如果们开启 返回
        {
            return;
        }
        if (main.toolType == ToolType.Hand)
        {
            //玻珠
            BeadClick();
        }
        if (main.toolType == ToolType.Brush)
        {
            PointColor();//涂色
        }
    }

    protected virtual void BeadClick()
    {
        if (!isClicked)
        {
            //没被点击过移动到目标位置
            if (num == 0)
            {
                transform.position = targetPos;
                isClicked = !isClicked;
                //beadGroup.AddBead(line, 1);
            }
            else
            {
                if (beadGroup.beadAllDown[line][num - 1].GetComponent<Bead>().isClicked)//找到对应的珠获取isClicked
                {
                    transform.position = targetPos;
                    isClicked = !isClicked;
                    //beadGroup.AddBead(line, 1);
                }
            }
        }
        else
        {
            //被点击过回到原来位置
            if (num == 3)
            {
                transform.position = startPos;
                isClicked = !isClicked;
                //beadGroup.AddBead(line, -1);
            }
            else
            {
                if (!beadGroup.beadAllDown[line][num + 1].GetComponent<Bead>().isClicked)//找到对应的珠获取isClicked
                {
                    transform.position = startPos;
                    isClicked = !isClicked;
                    //beadGroup.AddBead(line, -1);
                }
            }
        }
        beadGroup.TestOpenDoor();
    }

    protected void PointColor()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.color = main.handPoint.Find("Brush").GetComponentInChildren<Renderer>().materials[2].color;
        beadGroup.TestOpenDoor();//检测是否开门
    }
}
