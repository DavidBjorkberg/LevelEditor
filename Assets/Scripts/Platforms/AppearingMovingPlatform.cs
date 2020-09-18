using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingMovingPlatform : MovingPlatform
{
    protected void Start()
    {
        NodeInitialization();
    }
    public void Activate(float Duration)
    {
        StartMoving(Duration);
        GetComponentInChildren<AppearingGround>().Activate(Duration);
    }
}
