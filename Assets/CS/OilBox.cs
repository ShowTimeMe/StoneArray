using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBox : Machine
{
    protected override void OpenMachine()
    {
        Material mat = main.handPoint.Find("Brush").GetComponentInChildren<Renderer>().materials[2];//获取刷子的对应颜色
        mat.color = GetComponent<Renderer>().material.color;//吧当前油漆的严格给刷子
    }
}
