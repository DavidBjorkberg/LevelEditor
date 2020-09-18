using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : Gun
{

    public override IEnumerator StartShooting()
    {
        firing = true;
        while (firing)
        {
            yield return new WaitUntil(() => stats.Firerate > stats.StartFirerate);
            if (!firing)
            {
                break;
            }
            stats.Firerate = 0;
            RaycastHit2D HitCheck = Physics2D.Raycast(Gunpoint.position, weaponhandler.direction, 40, ~(transform.root.gameObject.layer | 1 << 15 | 1 << 16 | 1 << 17 | 1 << 18 | 1 << 22 | 1 << 25 | 1 << 0));
            float Distance;
            if (HitCheck)
            {
                Distance = HitCheck.distance;
                if (HitCheck.collider.tag.Contains("Player"))
                {
                    Main.KillPlayer(HitCheck.collider.gameObject, Player.gameObject);
                    Distance += HitCheck.collider.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                }
            }
            else
                Distance = weaponhandler.direction == Vector3.right ? 40 : -40;
            GameObject Ray = Instantiate(stats.Bullet, Gunpoint.position, Quaternion.identity);
            Ray.GetComponent<RayScript>().HitPos = HitCheck.point;
            Ray.GetComponent<RayScript>().Distance = Distance;
            Ray.SendMessage("SetShooter", weaponhandler, SendMessageOptions.RequireReceiver);
        }
    }
    public override string GetName()
    {
        return "Ray gun";
    }
}
