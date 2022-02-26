using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{

    protected bool isOpen;//机关是否开启
    protected MainCS main;
    public ToolType[] states;
    public GameObject beGetObj;
    // Start is called before the first frame update
    protected void Start()
    {
        main = MainCS.instance;
    }

    void OnMouseDown()
    {
        if (!main.TestDistance(transform.position, 3))//判断距离
        {
            print("too far");
            return;
        }
        if (isOpen)//判断机关是否开启
        {
            print("is   Opened");
            return;
        }
        if (!TestToolState())//判断状态
        {
            print("too; wrong");
            return;
        }
        OpenMachine();
    }

    protected virtual void OpenMachine()
    {
        string toolName = main.toolType.ToString();
        GameObject toolObj = main.handPoint.Find(toolName).gameObject;
        GameObject go = main.UseTool(toolName, toolObj);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        main.SetLayer(go.transform, "Default");
        isOpen = true;
        if (TestFinish())
        {
            beGetObj.GetComponent<Collider>().enabled = true;
        }
    }

    protected bool TestFinish()
    {
        foreach (Transform t in transform.parent)
        {
            if (!t.GetComponent<Machine>().isOpen)
            {
                return false;
            }
        }
        return true;
    }
    protected bool TestToolState()
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (main.toolType == states[i])
            {
                return true;
            }
        }
        return false;
    }



}
