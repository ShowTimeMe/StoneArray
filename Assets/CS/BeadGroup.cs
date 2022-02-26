using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadGroup : MonoBehaviour
{
    public static BeadGroup instance;
    public GameObject[] beadG0 = new GameObject[4];
    public GameObject[] beadG1 = new GameObject[4];
    public GameObject[] beadG2 = new GameObject[4];
    public GameObject[] beadG3 = new GameObject[4];
    public GameObject[] beadTop = new GameObject[4];
    public GameObject[][] beadAllDown = new GameObject[4][];
    public MainCS main;

    int answer ;
    int[] answers = new int[4] { 4, 0, 2, 2 };
    Color[] colors = new Color[4] { Color.red, Color.green, new Color(1,1,0), Color.black };

    public bool doorIsOpen { get; private set; }
    public Transform door;
    Vector3 startPos;
    Vector3 targetPos;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        beadAllDown[0] = beadG0;
        beadAllDown[1] = beadG1;
        beadAllDown[2] = beadG2;
        beadAllDown[3] = beadG3;
        startPos = door.position;
        targetPos = startPos + door.up * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorIsOpen)
        {
            if (main.TestDistance(startPos, 4))
            {
                door.position = Vector3.Lerp(door.position, targetPos, 0.04f);
            }
            else
            {
                door.position = Vector3.Lerp(door.position, startPos, 0.04f);
            }
        }
    }

    public void AddBead(int line, int value)//第一种最简单的
    {
        answer += (int)Mathf.Pow(10, 3 - line) * value;
        print(answer);
        if (answer == 9527)
        {
            print("right");
        }
    }
    bool TestBeadOk(int[] nums)//第二种方法下面珠的判定
    {
        for (int i = 0; i < nums.Length; i++)//循环数
        {
            if (nums[i] < beadAllDown[0].Length)//判断是不是最后一个
            {
                //           行    数组元素   
                if (beadAllDown[i][nums[i]].GetComponent<Bead>().isClicked)//判断是不是动了
                {
                    return false;
                }
            }
            if (nums[i] != 0)//判断当前珠不等于0
            {
                if (!beadAllDown[i][nums[i]-1].GetComponent<Bead>().isClicked)//是不是没动
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool TestBeadOk2(int[] nums,Color[] colors)//第三种方法下面珠的判定
    {
        for (int k = 0; k < nums.Length; k++)
        {
            for (int i = 0; i < nums[k]; i++)
            {
                if (!beadAllDown[k][i].GetComponent<Bead>().isClicked)
                {
                    return false;
                }
                if (!TestBeadColor(beadAllDown[k][i],colors[k]))
                {
                    print(k + "    " + i);
                    return false;
                }
            }
            for (int i = nums[k]; i < beadAllDown.Length; i++)
            {
                if (beadAllDown[k][i].GetComponent<Bead>().isClicked)
                {
                    return false;
                }
                if (!TestBeadColor(beadAllDown[k][i], Color.white))
                {
                    print(k + "    " + i);
                    return false;
                }
            }
        }
        return true;
    }

    bool TestBeadTopOk(bool[] array, Color[] colors)//上面珠的判定
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (beadTop[i].GetComponent<Bead>().isClicked != array[i])
            {
                return false;
            }
            if (!TestBeadColor(beadTop[i],colors[i]))
            {
                print(i);
                return false;
            }
        }
        return true;
    }

    bool TestBeadColor(GameObject bead, Color color)
    {
        if (bead.GetComponent<Renderer>().material.color == color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TestOpenDoor()
    {
        //第二种判定
        //if (TestBeadOk(answers)&& TestBeadTopOk(new bool[4] { true,true,false,true},new Color[4] { Color.red, Color.green, Color.white, Color.black }))
        //{
        //    print("1     " + true);
        //}
        //第三种判定
        if (!TestBeadOk2(answers,colors))
        {
            return;
        }
        if (!TestBeadTopOk(new bool[4] { true, true, false, true }, new Color[4] { Color.red, Color.green, Color.white, Color.black }))
        {
            return;
        }
        doorIsOpen = true;
        print("暗门打开");
    }
}
