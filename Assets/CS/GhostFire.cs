using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFire : MonoBehaviour
{
    Vector3 startPos;
    Vector3 targetPos;
    public  float r = 3;
    float minDis = 0.3f;
    float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetPos = RandomTargetPos();
        StartCoroutine(RandomSpeed());
    }

    IEnumerator RandomSpeed()
    {
        while (true)
        {
            speed = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(1);
        }
    }
    Vector3 RandomTargetPos()
    {
        return Random.insideUnitSphere * r + startPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPos, speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < minDis)
        {
            targetPos = RandomTargetPos();
        }
    }


}
