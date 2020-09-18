using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
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
            float FlashDuration = 0.55f;
            MuzzleflashFunc(FlashDuration);

            int BulletAmount = stats.BulletAmount;
            //Anglechange = Max Angle + Minimun angle. StartAngle = Max angle, -StartAngle = Minimum Angle
            float MaxAngle = stats.ShotAngle;
            float MinMaxAngleDistance = MaxAngle * 2;
            float Anglechange = MinMaxAngleDistance / BulletAmount;
            float StartAngle = MaxAngle + Anglechange;
            for (int i = 0; i < BulletAmount; i++)
            {
                GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint.position, Quaternion.identity);
                shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;
                shotbullet.GetComponent<ShotgunBullet>().Direction = Direction + new Vector3(0, StartAngle - Anglechange, 0);
                StartAngle -= Anglechange;
            }

        }
    }
    public override string GetName()
    {
        return "Shotgun";
    }
}
