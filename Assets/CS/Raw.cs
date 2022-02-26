using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raw : MonoBehaviour
{
    public GameObject arrowPrefab;//箭的预制体
    public  Transform point;//发射点
    Transform canvas;//画布
    Slider slider;//血条

    bool isDead;
    float time;
    float shootTime=1;//攻击间隔
    float attDis = 12;//攻击距离
    float hp = 100;//血量
    MainCS main;
    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        canvas = transform.Find("Canvas");//获取画布
        slider = canvas.GetComponentInChildren<Slider>();//找到血条
        slider.maxValue = hp;//最大血量
        slider.value = hp;//血量

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        canvas.LookAt(Camera.main.transform);
        slider.value += 0.1f;
        TestPlayer();
    }

    private void TestPlayer()//检测玩家
    {
        if (!main.TestDistance(transform.position, attDis))
        {
            return;
        }
        Vector3 dir = main.Player.position - transform.position;//向量
        Ray ray = new Ray(transform.position, dir);//实例化射线
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attDis))
        {
            if (hit.collider.tag == "Player")
            {
                Shoot(dir);
            }
        }
    }

    private void Shoot(Vector3 dir)//射击
    {
        Quaternion euler = Quaternion.LookRotation(-dir);//角度
        transform.rotation = Quaternion.Lerp(transform.rotation, euler, 0.04f);//转向
        if (Vector3.Angle(transform.forward, -dir) > 5)//判定
        {
            return;
        }
        if (Time.time - time < shootTime)
        {
            return;
        }
        time = Time.time;
        GameObject arrow = Instantiate(arrowPrefab, point.position, point.rotation);//实例化弩箭
        arrow.GetComponent<Rigidbody>().AddForce(point.forward*-1000);//添加力发射弩箭
        Destroy(arrow, 3);
    }

    public void RawHurt(float f)
    {
        if (isDead)
        {
            return;
        }
        slider.value -= f;
        if (slider.value == 0)
        {
            isDead = true;
            Destroy(canvas.gameObject);//销毁血条
        }
    }
}
