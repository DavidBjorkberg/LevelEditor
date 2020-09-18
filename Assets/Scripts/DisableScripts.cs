using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DisableScripts : MonoBehaviour
{

    void Update()
    {
        if (!Application.isPlaying)
        {
            if (GetComponent<WallTurret>().ShootDirection == WallTurret.ShootDir.Custom)
            {
                GetComponent<VerticalPositioning>().enabled = false;
                GetComponent<HorizontalPositioning>().enabled = false;
            }
            else
            {
                GetComponent<VerticalPositioning>().enabled = true;
                GetComponent<HorizontalPositioning>().enabled = true;
            }
        }
    }
}
