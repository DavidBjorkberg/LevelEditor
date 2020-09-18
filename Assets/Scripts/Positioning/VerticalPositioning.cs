using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class VerticalPositioning : MonoBehaviour
{
    public GameObject parentGO;
    Vector3 topOfGO;
    float height;
    Vector3 bottomOfGO;
    public bool snapToRoof;
    void Start()
    {
        if (GetComponent<WallTurret>() != null)
        {
            height = transform.GetChild(1).GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else if (GetComponent<SpriteRenderer>() != null)
        {
            height = GetComponent<SpriteRenderer>().bounds.size.y;
        }


        if (!Application.isPlaying)
        {
            transform.SetParent(GameObject.Find("Scene props").transform);
        }
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            bottomOfGO = new Vector3(transform.position.x, transform.position.y - height / 2, 0);
            topOfGO = new Vector3(transform.position.x, transform.position.y + height / 2, 0);

            SnapToTerrain(bottomOfGO, Vector3.down);
            if (snapToRoof)
            {
                SnapToTerrain(topOfGO, Vector3.up);
            }
        }
    }
    void SnapToTerrain(Vector3 startPos, Vector3 direction)
    {
        RaycastHit2D dirCheck = Physics2D.Raycast(startPos, direction, 0.4f, 1 << 8);

        if (dirCheck)
        {
            float hitYPosDelta = startPos.y - dirCheck.point.y;
            transform.position -= new Vector3(0, hitYPosDelta);
            if(parentGO != null)
            {
                parentGO.transform.position = transform.position;
            }
        }
    }
}