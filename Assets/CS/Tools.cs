using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    GameObject btnTool;
    MainCS main;
    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        btnTool = Resources.Load<GameObject>("Prefab/Btn" + name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //检测距离  当前主角 和物体的距离
        if (!main.TestDistance(transform.position, 3))
        {
            print("too far");
            return;
        }
        if (!main.IsHand())
        {
            print("tool wrong");
            return;
        }

        if (main.handPoint.Find(name))
        {
            BtnTool btn = main.BagPanel.Find(btnTool.name).GetComponent<BtnTool>();
            btn.AddToolNum(btn.toolGetNum);
            Destroy(gameObject);
        }
        else
        {
            PickUp();
        }
       
    }

    public void PickPic()
    {
        PickUp();
    }

    void PickUp()
    {
        main.SetLayer(transform,"Arm");//改变层
        transform.position = main.handPoint.position;
        transform.SetParent(main.handPoint, true);//保持自身大小  不随父物体缩放
        transform.localEulerAngles = Vector3.zero;//跟随父物体角度
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;//关掉collider
        enabled = false;
        GameObject btn = Instantiate(btnTool, main.BagPanel);//背包生成
        btn.name = btnTool.name;//名字
    }
}
