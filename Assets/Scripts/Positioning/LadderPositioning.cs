using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LadderPositioning : MonoBehaviour
{

    float upDistance;
    float downDistance;
    internal GameObject topPlayerCheck;
    internal GameObject bottomPlayerCheck;
    private int maxLadderSize = 20;
    private void Start()
    {
        topPlayerCheck = transform.GetChild(0).gameObject;
        bottomPlayerCheck = transform.GetChild(1).gameObject;
        transform.SetParent(GameObject.Find("Scene props").transform);

    }
    void Update()
    {
        PositionLadder();

    }
    void PositionLadder()
    {
        if (!Application.isPlaying)
        {
            RaycastHit2D upCheck = Physics2D.Raycast(transform.position, Vector2.up, maxLadderSize / 2, 1 << 8);
            RaycastHit2D downCheck = Physics2D.Raycast(transform.position, Vector2.down, maxLadderSize / 2, 1 << 8);
            if (upCheck && downCheck)
            {
                if (upCheck.collider.GetComponent<SpriteRenderer>() != null)
                {
                    upDistance = Vector3.Distance(new Vector2(0,
                        upCheck.point.y + upCheck.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f)
                        , new Vector2(0, transform.position.y));

                    downDistance = Vector3.Distance(new Vector2(0, downCheck.point.y), new Vector2(0, transform.position.y));

                    if (upDistance != 0 && downDistance != 0)
                    {
                        Vector3 TopPos = new Vector3(transform.position.x, upCheck.point.y + upCheck.collider.GetComponent<SpriteRenderer>().bounds.size.y);
                        transform.position = Vector3.Lerp(TopPos, downCheck.point, 0.5f);
                        float ladderHeight = Mathf.Abs(TopPos.y - downCheck.point.y);
                        GetComponent<SpriteRenderer>().size = new Vector3(0.8f, ladderHeight);
                        float topYPos = (ladderHeight / 2);
                        bottomPlayerCheck.transform.localPosition = new Vector3(bottomPlayerCheck.transform.localPosition.x, -topYPos);
                        topPlayerCheck.transform.localPosition = new Vector3(topPlayerCheck.transform.localPosition.x, topYPos);
                    }
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().size = new Vector3(0.8f, 1);
            }
        }
    }
}
