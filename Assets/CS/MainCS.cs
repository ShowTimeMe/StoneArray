using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ToolType
{ BallRed,BallGreen,BallYellow,BallWhite,BallBlack,
  BallStone,PicYZ,PicYX,PicYC,PicZF,
  Brush,Bomb,Torch,HealthBox,Hand
};



public class MainCS : MonoBehaviour
{
    public static MainCS instance;//单例
    public Transform Player;//人物
    public Transform handPoint;//手
    public Transform BagPanel;//背包面板

    public ToolType toolType = ToolType.Hand;

    public Transform picDoor;

    private void Awake()
    {
        instance = this;
        //Cursor.lockState = CursorLockMode.Locked;//隐藏鼠标指针
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public bool TestDistance(Vector3 pos, float distance)
    {
        return Vector3.Distance(Player.position, pos) < distance;
    }

    public bool IsHand()
    {
        return toolType == ToolType.Hand;
    }

    public void SetLayer(Transform t,string n)//改变层
    {
        Transform[] goes = t.GetComponentsInChildren<Transform>();
        foreach (Transform go in goes)
        {
            go.gameObject.layer = LayerMask.NameToLayer(n);//根据名字设定层
        }
    }
    public void  AllToolsVisible(bool b)
    {
        foreach (Transform t in handPoint)
        {
            t.gameObject.SetActive(b);
        }
    }

    public GameObject UseTool(string toolName, GameObject obj)//使用工具
    {
        BtnTool btnTool = BagPanel.Find("Btn" + toolName).GetComponent<BtnTool>();//找到道具所对应的按钮
        if (btnTool.GetToolNum() > 0)
        {
            GameObject go = null;
            if (obj)
            {
                go = Instantiate(obj);//复制
                //go.name = toolName;
            }
            btnTool.AddToolNum(-1);
            if (btnTool.GetToolNum() == 0)
            {
                Destroy(handPoint.Find(toolName).gameObject);//销毁按钮
                Destroy(btnTool.gameObject, 0.1f);//销毁道具
                toolType = ToolType.Hand;
            }
            return go;
        }
        else
        {
            return null;
        }
    }

    public void Hand()//空手
    {
        toolType = ToolType.Hand;
        AllToolsVisible(false);//所有物品隐藏
    }
}
