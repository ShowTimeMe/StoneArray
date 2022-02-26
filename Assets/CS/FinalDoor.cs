using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : Kuang
{
    Transform door;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        door = transform.parent;
        doorStartPos = door.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            door.position = Vector3.Lerp(door.position, doorStartPos + door.forward * 2, 0.04f);
        }
    }
}
