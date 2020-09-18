using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class DisablePositioning : MonoBehaviour
{
    Vector3 CurPos;
    Vector3 Temp;
    Vector3 SelectedTemp;
    Vector3 SelectedPos;
    bool SelectedMoving;
    void Update()
    {
        if (!Application.isPlaying)
        {
            Temp = CurPos;
            CurPos = transform.position;
            if (Selection.activeGameObject != null)
            {
                SelectedTemp = SelectedPos;
                SelectedPos = Selection.activeGameObject.transform.position;
                SelectedMoving = SelectedPos != SelectedTemp;
            }
            else
                SelectedMoving = true;

            bool Moving = CurPos != Temp;
            if (Moving || !SelectedMoving || Selection.activeGameObject == null)
            {
                if (GetComponent<VerticalPositioning>() != null)
                {
                    GetComponent<VerticalPositioning>().enabled = true;

                }
                if (GetComponent<HorizontalPositioning>() != null)
                {

                    GetComponent<HorizontalPositioning>().enabled = true;
                }
                if (GetComponent<DoorPositioning>() != null)
                {

                    GetComponent<DoorPositioning>().enabled = true;
                }

            }
            else
            {

                if (GetComponent<VerticalPositioning>() != null)
                {
                    GetComponent<VerticalPositioning>().enabled = false;

                }
                if (GetComponent<HorizontalPositioning>() != null)
                {

                    GetComponent<HorizontalPositioning>().enabled = false;
                }
                if (GetComponent<DoorPositioning>() != null)
                {

                    GetComponent<DoorPositioning>().enabled = false;
                }
            }
        }

    }
}
