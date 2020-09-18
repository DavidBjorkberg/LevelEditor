using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawLinesMovingPlatform : MonoBehaviour
{
    DrawLines drawlines;
    private void Start()
    {
        drawlines = FindObjectOfType<DrawLines>();
        transform.SetParent(GameObject.Find("Scene props").transform);
    }
    void Update()
    {
        if (drawlines.DirectionLines)
        {
            NodeSpawner MP = GetComponent<NodeSpawner>();
            if (MP.nodes.Count > 0)
            {
                for (int i = 0; i < MP.nodes.Count - 1; i++)
                {
                    Debug.DrawLine(MP.nodes[i].transform.position, MP.nodes[i + 1].transform.position);
                }
                if (GetComponent<MovingPlatform>().Circulate)
                {
                    Debug.DrawLine(MP.nodes[MP.nodes.Count - 1].transform.position, MP.nodes[0].transform.position);
                }
            }
        }
    }
}
