using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrenade : ThrowableObject
{
    protected override void BulletShot(GameObject Grenade)
    {
        Grenade.GetComponent<GrenadeBullet>().Direction = Direction;
        Grenade.GetComponent<GrenadeBullet>().shooter = weaponhandler;
        Grenade.GetComponent<GrenadeBullet>().Thrown = true;
    }
    public override string GetName()
    {
        return "Hand grenade";
    }
}
