using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class WallTurretRotation : MonoBehaviour
{
    WallTurret.ShootDir Temp;
    WallTurret.ShootDir Cur;
    void Update()
    {
        if (!Application.isPlaying)
        {
            GameObject ThingRotater = transform.GetChild(0).gameObject;
            Vector3 meshSize = transform.GetChild(1).GetComponent<SpriteRenderer>().bounds.size;
            Temp = Cur;         
            Cur = GetComponent<WallTurret>().ShootDirection;
            if (Cur != Temp)
            {
                switch (GetComponent<WallTurret>().ShootDirection)
                {

                    case WallTurret.ShootDir.Left:
                        transform.rotation = Quaternion.identity;
                        ThingRotater.transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case WallTurret.ShootDir.Right:
                        transform.rotation = Quaternion.identity;
                        ThingRotater.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case WallTurret.ShootDir.Up:
                        transform.rotation = Quaternion.identity;
                        ThingRotater.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case WallTurret.ShootDir.Down:
                        transform.rotation = Quaternion.identity;
                        ThingRotater.transform.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                    case WallTurret.ShootDir.Custom:
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
