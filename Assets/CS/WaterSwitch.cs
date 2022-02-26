using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSwitch : Machine
{
    public Transform waterPlane;
    Vector3 targetPos;
    Vector3 waterTargetPos;

    protected void Start()
    {
        base.Start();//执行父类start
        targetPos = transform.position - Vector3.up * 0.05f;
        waterTargetPos = waterPlane.position - Vector3.up * 0.4f;
    }

    private void Update()
    {
        if (isOpen)
        {
            waterPlane.position = Vector3.Lerp(waterPlane.position,waterTargetPos,0.005f);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.005f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,200,0), 0.005f);
        }
    }

    protected override void OpenMachine()
    {
        isOpen = true;
    }
}
