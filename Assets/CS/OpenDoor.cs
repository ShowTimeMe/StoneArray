using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Machine
{

    protected override void OpenMachine()
    {
        GetComponentInParent<DoorKey>().isOpen = true;
        GetComponent<Renderer>().enabled = true;
    }

}
