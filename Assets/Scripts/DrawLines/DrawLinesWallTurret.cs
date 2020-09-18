using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawLinesWallTurret : MonoBehaviour
{
    DrawLines drawlines;
    GameObject Thing;
    void Start()
    {
        drawlines = FindObjectOfType<DrawLines>();
        Thing = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (drawlines.DirectionLines)
        {
            RaycastHit2D DistanceCheck = Physics2D.Raycast(transform.position, Thing.transform.right, 30, 1 << 8 | 1 << 20 | 1 << 21);
            float Distance = 30;
            if (DistanceCheck)
            {
                Distance = Vector3.Distance(transform.position, DistanceCheck.point);
            }
            Debug.DrawRay(transform.position, Thing.transform.right * Distance, Color.green);
        }
    }
}
