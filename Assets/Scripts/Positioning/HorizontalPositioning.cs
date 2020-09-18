using UnityEngine;
[ExecuteInEditMode]
public class HorizontalPositioning : MonoBehaviour
{
    float Distance;
    float Width;
    Vector3 Left;
    Vector3 Right;

    void Start()
    {
        if (name.Contains("turret"))
            Width = transform.GetChild(1).GetComponent<SpriteRenderer>().bounds.size.x;
        else
        Width = GetComponent<SpriteRenderer>().bounds.size.x;
        if (!Application.isPlaying)
        {
            transform.SetParent(GameObject.Find("Scene props").transform);
        }
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            Left = new Vector3(transform.position.x - Width / 2, transform.position.y, 0);
            Right = new Vector3(transform.position.x + Width / 2, transform.position.y, 0);
            RaycastHit2D LeftCheck = Physics2D.Raycast(Left, -transform.right, 0.6f, 1 << 21 | 1 << 8);
            RaycastHit2D RightCheck = Physics2D.Raycast(Right, transform.right, 0.6f, 1 << 21 | 1 << 8);

            if (RightCheck && LeftCheck)
            {
                float LeftDistance = Vector3.Distance(new Vector2(0, LeftCheck.point.x), new Vector2(0, Left.x));
                float RightDistance = Vector3.Distance(new Vector2(0, RightCheck.point.x), new Vector2(0, Right.x));
                if (LeftDistance < RightDistance)
                {
                    HitLeft(LeftCheck);
                }
                else
                {
                    HitRight(RightCheck);
                }
            }
            else if (LeftCheck)
            {
                HitLeft(LeftCheck);
            }
            else if (RightCheck)
            {
                HitRight(RightCheck);
            }
        }
    }
    void HitRight(RaycastHit2D RightCheck)
    {
        Distance = Vector3.Distance(new Vector2(0, RightCheck.point.x), new Vector2(0, Right.x));
        if (Distance > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x + Distance, transform.position.y), 1);
        }
    }
    void HitLeft(RaycastHit2D LeftCheck)
    {
        Distance = Vector3.Distance(new Vector2(0, LeftCheck.point.x), new Vector2(0, Left.x));
        if (Distance > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x - Distance, transform.position.y), 1);
        }
    }
}
