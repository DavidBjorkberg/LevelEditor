using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DoorPositioning : MonoBehaviour
{
    GameObject Top;
    GameObject Bottom;
    GameObject GapFiller;
    GameObject GapBottom;
    float DoorDistance;
    SpriteRenderer sprite;
    bool Positioned;
    void Start()
    {
        Top = transform.Find("Top").gameObject;
        Bottom = transform.Find("Bottom").gameObject;
        GapFiller = transform.Find("GapFiller").gameObject;
        GapBottom = GapFiller.transform.Find("GapBottom").gameObject;
        transform.SetParent(GameObject.Find("Scene props").transform);
        sprite = GapFiller.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            RaycastHit2D UpCheck = Physics2D.Raycast(Top.transform.position, Vector2.up, 1.5f, 1 << 8 | 1 << 21);
            RaycastHit2D DownCheck = Physics2D.Raycast(Bottom.transform.position, Vector2.down, 1.5f, 1 << 8);

            if (UpCheck && DownCheck)
            {
                DoorDistance = Vector3.Distance(new Vector2(0, DownCheck.point.y), new Vector2(0, Bottom.transform.position.y));
                if (DoorDistance > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - DoorDistance);
                    if (!Positioned)
                    {
                        RaycastHit2D GapUpCheck =  ClosestColl();
                        RaycastHit2D GapDownCheck = Physics2D.Raycast(GapBottom.transform.position, Vector2.down, 3, 1 << 20);
                        if (GapUpCheck && GapDownCheck)
                        {
                            float Distance = Vector3.Distance(new Vector3(0, GapUpCheck.point.y), new Vector3(0, GapDownCheck.point.y));
                            GapFiller.transform.position = Vector3.Lerp(GapUpCheck.point, GapDownCheck.point, 0.5f);
                            GapFiller.GetComponent<SpriteRenderer>().size = new Vector3(0.32f, Distance * 2.52075839126f);
                            GapFiller.GetComponent<BoxCollider2D>().size = new Vector3(0.32f, Distance * 2.52075839126f);
                            Positioned = true;
                        }

                    }


                }
            }
            else
            {
                GapFiller.GetComponent<SpriteRenderer>().size = new Vector3(0.32f, 0.5f, 1);
                GapFiller.GetComponent<BoxCollider2D>().size = new Vector3(0.32f, 0.5f, 1);
                GapFiller.transform.localPosition = new Vector2(0, 0.352f);
                Positioned = false;
            }
        }
    }
    RaycastHit2D ClosestColl()
    {
        Vector3 TopPos = sprite.bounds.center + new Vector3(0, (sprite.bounds.size.y / 2) + 0.2f);

        //Ta bort sig själv, ta bort alla utom den närmaste
        RaycastHit2D[] GapUpCheck = Physics2D.RaycastAll(TopPos, Vector2.up, 3, 1 << 8 | 1 << 21);
        List<RaycastHit2D> Hits = new List<RaycastHit2D>(GapUpCheck);
        foreach (RaycastHit2D Hit in Hits)
        {
            if (Hit.collider.gameObject == GapFiller)
            {
                Hits.Remove(Hit);
                break;
            }
        }
        RaycastHit2D Closest = new RaycastHit2D();
        foreach (RaycastHit2D Hit in Hits)
        {
            if (Hit.distance < Closest.distance || Closest.collider == null)
            {
                Closest = Hit;
            }
        }
        return Closest;
    }
}