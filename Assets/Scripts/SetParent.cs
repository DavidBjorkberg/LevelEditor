using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SetParent : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(GameObject.Find("Scene").transform);

    }
}
