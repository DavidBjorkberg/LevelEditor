using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurret : MonoBehaviour
{
    public enum ShootDir
    {
        Left, Right, Up, Down, Custom
    }
    public GameObject Bullet;
    public float BulletSpeed;
    public float FiringSpeed;
    [Tooltip("0 = Infinite")]
    public float ShootTime;
    public ShootDir ShootDirection;
    public bool ShootOnGameStart;
    protected bool Shooting;
    internal GameObject Target;
    protected GameObject aimBarrel;
    void Start()
    {
        Invoke("ShootOnStart", FiringSpeed);
        aimBarrel = transform.GetChild(0).gameObject;
    }

    private void ShootOnStart()
    {
        if (ShootOnGameStart)
        {
            StartShooting();
            if (ShootTime != 0)
            {
                Invoke("StopShooting", ShootTime);
            }
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject FiredBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            FiredBullet.GetComponent<WallTurretBullet>().bulletspeed = BulletSpeed;
            FiredBullet.GetComponent<WallTurretBullet>().Direction = aimBarrel.transform.right;
            yield return new WaitForSeconds(FiringSpeed);
        }


    }
    public virtual void OnButtonClick(float Alivetime)
    {
        StartShooting();
        if (Alivetime != 0)
        {
            Invoke("StopShooting", Alivetime);
        }
    }

    public void StartShooting()
    {
        Shooting = true;
        StartCoroutine("Shoot");
    }
    public void StopShooting()
    {
        Shooting = false;
        StopCoroutine("Shoot");
    }
}
