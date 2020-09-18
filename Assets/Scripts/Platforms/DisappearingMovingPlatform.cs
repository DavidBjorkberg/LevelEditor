using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingMovingPlatform : MovingPlatform
{
    protected void Start()
    {
        NodeInitialization();
        StartCoroutine("Move");
    }
    public void Activate(float Duration)
    {
        GetComponentInChildren<DisappearingGround>().Activate(Duration);
        StopMoving();
        Invoke("StartMoving", Duration);
    }
    void StartMoving()
    {
        StartCoroutine("Move");
    }
}
