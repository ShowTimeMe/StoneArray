using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnTool : MonoBehaviour
{
    public int toolGetNum;
    Text numText;
    int toolNum;
    MainCS main;



    private void Awake()
    {
        numText = GetComponentInChildren<Text>();
        toolNum = toolGetNum;
        numText.text = toolNum.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        GetComponent<Button>().onClick.AddListener(BtnClick);
    }

    void BtnClick()
    {
        //if (!main.IsHand())
        //{
        //    string tool= main.toolType.ToString();
        //    ShowTool(tool,false);
        //}
        string toolName = name.Remove(0, 3);//删掉前三个字符
        main.toolType = (ToolType)Enum.Parse(typeof(ToolType), toolName);//强制转换  把字符串强转乘枚举
        
        ShowTool(toolName, true);
        //显示
        //string[] toolTypes = Enum.GetNames(typeof(ToolType));//把枚举力的所有内容变成字符串数组
        //for (int i = 0; i < toolTypes.Length; i++)
        //{
        //    if (toolName == toolTypes[i])//下标
        //    {
        //        main.toolType = (ToolType)i;
        //        break;
        //    }
        //}
        
        
    }

    void ShowTool(string n,bool b)
    {
       
        main.AllToolsVisible(false);//隐藏所有
        GameObject tool = main.handPoint.Find(n).gameObject;//找到道具
        tool.SetActive(true);//显示
    }

    public int GetToolNum()//只读
    {
        return toolNum;
    }
    public void AddToolNum(int num)//添加道具数量
    {
        toolNum += num;
        numText.text = toolNum.ToString();
    }
}