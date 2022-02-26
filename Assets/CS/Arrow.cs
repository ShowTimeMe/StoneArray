using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//碰到主角   找到脚本  调用减血 销毁自身
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.SliderNum(player.hpline, -10);
            Destroy(gameObject);
        }
    }
}
