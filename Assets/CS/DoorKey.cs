using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Door
{
    public bool isOpen;

   
    void Update()
    {
        if (!isOpen)
        {
            return;
        }
        OpenDoor();
    }
}
