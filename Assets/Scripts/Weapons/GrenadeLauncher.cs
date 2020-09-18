using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun
{
    protected override void BulletShot(GameObject bullet)
    {
        bullet.GetComponent<GrenadeBullet>().Direction = Direction + new Vector3(0, stats.ShotAngle, 0);
    }
    public override string GetName()
    {
        return "Grenade launcher";
    }
}
