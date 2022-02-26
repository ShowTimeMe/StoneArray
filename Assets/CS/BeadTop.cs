using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadTop : Bead
{
    // Start is called before the first frame update
    void Start()
    {
        main = MainCS.instance;
        beadGroup = BeadGroup.instance;
        SetStartValue(transform.forward*0.15f);
    }
    
    protected override void BeadClick()
    {
        if (!isClicked)
        {
            transform.position = targetPos;
            //beadGroup.AddBead(line, 5);
        }
        else
        {
            transform.position = startPos;
            //beadGroup.AddBead(line, -5);
        }
        isClicked = !isClicked;
        beadGroup.TestOpenDoor();
    }
}
