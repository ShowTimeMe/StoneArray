using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController cc;//角色控制器
    Transform eye;//眼睛
    float speedMove=6;
    float speedH = 0.6f;
    const float G = -9.8f;

    float angleSpeed = 120;
    float minAngle = -40;
    float maxAngle = 70;
    float yRote;

    float hp = 100;
    public Slider hpline;
    public Slider powerline;
    public GameObject bombPrefab;

    MainCS main;

    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        cc = GetComponent<CharacterController>();
        eye = transform.Find("Eye");
    }

    // Update is called once per frame
    void Update()
    {
        PowerlineCtrl();
        Move();
        UseHandTool();
    }

    void Move()
    {
        cc.Move(transform.up * G * Time.deltaTime);//始终受到重力影响
        if (powerline.value < 1)
        {
            return;
        }
        float y = Input.GetAxis("Vertical");
        float x=Input.GetAxis("Horizontal");
        cc.Move(transform.forward * speedMove * y * Time.deltaTime);//前后移动
        cc.Move(transform.right * speedMove * x * Time.deltaTime*speedH);//横向移动
        if (Input.GetMouseButton(1))
        {
            //转身
            float xRote = Input.GetAxis("Mouse X");
            transform.Rotate(transform.up * angleSpeed * xRote * Time.deltaTime);
            //抬头低头
            yRote -= Input.GetAxis("Mouse Y");
            yRote = Mathf.Clamp(yRote, minAngle, maxAngle);
            eye.localEulerAngles = new Vector3(yRote,0,0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void PowerlineCtrl()//控制体力条
    {
        if (Input.anyKey)
        {
            SliderNum(powerline, -0.1f);//按下任意键 减0.1

        }
        else
        {
            SliderNum(powerline, 0.3f);//体力回复
        }
    }

    public void SliderNum(Slider slider,float f)
    {
        slider.value += f;
        if (slider.name=="SliderHp"&&slider.value==0)
        {
            //游戏结束
            SceneManager.LoadScene("GameOver");
        }
    }

    void UseHandTool()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (main.toolType == ToolType.Bomb)
            {
                ThrowBomb();
            }
            if (main.toolType == ToolType.HealthBox)
            {
                SliderNum(hpline, 50);
                main.UseTool("HealthBox", null);
            }
        }
    }

    void ThrowBomb()
    {
        GameObject go =main.UseTool("Bomb", bombPrefab);//实例化
        go.transform.position =main.handPoint.position;//坐标赋值
        go.GetComponent<Rigidbody>().AddForce(eye.transform.forward * 600);//添加力
    }
}
