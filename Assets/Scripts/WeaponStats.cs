using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public enum Weapon_type
    {
        Rifle, Shotgun, GrenadeLauncher, RocketLauncher, RayGun, Melee, Watergun, Throw
    }
    public Weapon_type WeaponType;
    public GameObject Bullet;
    public int Range;
    public int bulletspeed;
    public float Firerate;
    public float ExplosionRadius;
    internal float StartFirerate;
    public float ShotAngle;
    public int BulletAmount;
    //Grenade launcher
    public float yTarget;
    public float ySmoothtime;
    public float yMaxSpeed;
    public float xTarget;
    public float xSmoothtime;
    public float xMaxSpeed;
    public float ExplodeTime;
    private void Start()
    {
        StartFirerate = Firerate;
    }

}
