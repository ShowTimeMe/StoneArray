using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Machine
{
    public Transform otherLion;
    protected override void OpenMachine()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 45, 0);//角度增加
        Test();
    }

    void Test()
    {
        if (SetDirction()&&otherLion.GetComponent<Lion>().SetDirction())//两个都为真
        {
            isOpen = true;
            otherLion.GetComponent<Lion>().isOpen = true;
            beGetObj.SetActive(false);//对应的物体
        }
    }

    public bool SetDirction()
    {
        Vector3 dir = otherLion.position - transform.position;//得出向量
        if (Mathf.RoundToInt(Vector3.Angle(dir, transform.right)) < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
