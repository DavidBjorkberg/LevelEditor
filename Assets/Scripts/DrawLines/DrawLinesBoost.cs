using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawLinesBoost : MonoBehaviour
{
    Boost boost;
    DrawLines drawlines;
    void Start()
    {
        drawlines = FindObjectOfType<DrawLines>();
        boost = GetComponent<Boost>();
    }

    void Update()
    {
        if (drawlines.DirectionLines)
        {
            if (boost.BoostHeight == 10)
            {
                Debug.DrawRay(transform.position, transform.up * boost.BoostHeight * 0.6f, Color.red);
            }
            if (boost.BoostHeight > 10)
            {
                Debug.DrawRay(transform.position, transform.up * boost.BoostHeight + new Vector3(0, (0.34f * boost.BoostHeight - 10)) * 0.6f, Color.red);

            }
            if (boost.BoostHeight < 10)
            {
                Debug.DrawRay(transform.position, transform.up * boost.BoostHeight * 0.6f + new Vector3(0, (-0.2f)), Color.red);
            }
        }


    }
}
