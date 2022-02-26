using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuang : Machine
{
    protected Vector3 doorStartPos;
    Vector3 startPos = new Vector3(0,-0.055f,-0.03f);//放在筐里的位置

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        doorStartPos = main.picDoor.position;//拿到门的位置
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            main.picDoor.position = Vector3.Lerp(main.picDoor.position, doorStartPos - main.picDoor.up*3,0.04f);
        }
    }

    protected override void OpenMachine()
    {
        if (transform.childCount == 0)
        {
            if (main.toolType != ToolType.Hand)
            {
                //把画放上去
                PickPic();
            }
        }
        else
        {
            if (main.toolType == ToolType.Hand)
            {
                //调用取画方法
                GetComponentInChildren<Tools>().PickPic();
            }
        }

        //检测机关是否开启
        if (TestAllPic())
        {
            print("机关开启");
            SetAllPicOK();
        }
    }

    void PickPic()
    {
        string str = main.toolType.ToString();//获得字符串
        Transform  t = main.handPoint.Find(str);//查找子物体
        t.parent = transform;//赋值父物体
        t.localEulerAngles = Vector3.zero;//角度
        t.localPosition = startPos;//位置
        //t.GetComponent<Tools>().enabled = true;//脚本开关
        Transform btnT = main.BagPanel.transform.Find("Btn" + str);
        main.toolType = ToolType.Hand;
        main.SetLayer(t, "Default");
        Destroy(btnT.gameObject);
    }

    protected bool TestAllPic()//检测函数
    {
        foreach (Transform t in transform.parent)//判断
        {
            if (t.childCount == 0)
            {
                return false;
            }
            if (t.name != "K" + t.GetChild(0).name)
            {
                return false;
            }
        }
        return true;
    }

    protected void SetAllPicOK()
    {
        foreach (Transform t in transform.parent)
        {
            t.GetComponent<Kuang>().isOpen = true;
        }
    }
}
