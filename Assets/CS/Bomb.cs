using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombEffect;
    float attDis=10;
    float attMax=100;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BombEffect", 3);
        Destroy(gameObject, 3);
    }

    void BombEffect()
    {
        GameObject effect = Instantiate(bombEffect, transform.position, Random.rotation);
        Collider[] rawColl = Physics.OverlapSphere(transform.position, attDis, LayerMask.GetMask("Raw"));
        foreach(Collider c in rawColl)
        {
            c.GetComponent<Raw>().RawHurt(Hurt(c.gameObject));
        }
        Destroy(effect, 4);
    }

    float Hurt(GameObject go)
    {
        float disTance = Vector3.Distance(transform.position, go.transform.position);
        if (disTance >= attDis)
        {
            return 0;
        }
        else
        {
            return attMax - disTance * attMax / attDis;
        }
    }
}
