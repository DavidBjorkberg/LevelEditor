using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ladder : MonoBehaviour
{
    internal Vector3 Top;
    internal Vector3 Bottom;
    internal Vector3 TopCheck;
    internal Vector3 BottomCheck;
    private void Start()
    {
        TopCheck = transform.GetChild(0).position;
        BottomCheck = transform.GetChild(1).position;
    }
}
