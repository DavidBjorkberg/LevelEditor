using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWallTurret : WallTurret
{
    bool activated;
    public LayerMask hitLayers;
    void Update()
    {
        if (activated || ShootOnGameStart)
        {
            LookForTarget();
        }
    }
    void LookForTarget()
    {
        if (Target != null)
        {
            Vector3 direction = Target.transform.position - transform.position;
            RaycastHit2D HitCheck = Physics2D.Raycast(transform.position, direction, 30, hitLayers);
            if (HitCheck)
            {
                if (!HitCheck.collider.CompareTag("Player"))
                {
                    Target = null;
                }
            }
            if (!Shooting)
            {
                Shooting = true;
                Invoke("StartShooting", FiringSpeed);
            }
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
            aimBarrel.transform.rotation = rotation;
        }
        else
        {
            StopShooting();
        }
    }
    public override void OnButtonClick(float aliveTime)
    {
        if (aliveTime != 0)
        {
            Invoke("StopShooting", aliveTime);
        }
        activated = true;
    }
    void Deactivate()
    {
        activated = false;
    }
}
