using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    internal WeaponHandler shooter;
    Vector3 StartDir;
    internal float Distance;
    internal Vector3 HitPos;
    Vector3 Startpos;
    Vector3 CurPos;
    void Start()
    {
        StartDir = shooter.direction;
        float LifeTime = 0.2f;
        Destroy(gameObject, LifeTime);
        Startpos = transform.position;
        CurPos = Startpos;
        PositionAndScale();
    }

    void Update()
    {
        if (shooter.direction == StartDir)
        {
            transform.position -= (CurPos - shooter.EquippedGun.GetComponent<Weapon>().Gunpoint.position        );
            CurPos = shooter.EquippedGun.GetComponent<Weapon>().Gunpoint.position;
        }
    }
    void SetShooter(WeaponHandler Shooter)
    {
        shooter = Shooter;
    }
    void PositionAndScale()
    {
        float RayHeight = 1.78f;
        if (HitPos != Vector3.zero)
            transform.position = Vector3.Lerp(Startpos, HitPos, 0.5f);
        else
            transform.position = Vector3.Lerp(Startpos, new Vector3(Startpos.x + Distance, Startpos.y), 0.5f);
        GetComponent<SpriteRenderer>().size = new Vector3(Distance / 2, RayHeight);
    }
}
