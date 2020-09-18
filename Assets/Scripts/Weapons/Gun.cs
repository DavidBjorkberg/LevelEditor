using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon
{
    protected Vector3 Direction;
    Vector3 DirectionInput;

    public virtual void Update()
    {
        if (transform.root.tag.Contains("Player"))
        {
            Aim();
        }
    }
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

            GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint.position, Quaternion.identity);
            shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;
            shotbullet.GetComponent<PlayerBullet>().Direction = Direction;
            BulletShot(shotbullet);
        }
    }
    float verticalInput;
    float horizontalInput;
    public virtual void Aim()
    {
        bool FacingLeft = Player.direction == Vector3.left;
        //Keyboard
        if (Player.allowedinput.Contains("Keyboard"))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos = new Vector3(MousePos.x, MousePos.y, 0);
            Direction = (MousePos - transform.position).normalized;
            Direction = new Vector3(FacingLeft ? -Direction.x : Direction.x, Direction.y);
            float MinClamp = -90;
            float MaxClamp = 90;
            float angle = Mathf.Clamp(Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg, MinClamp, MaxClamp);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        }
        //Controller
        else
        {
            verticalInput = Input.GetAxis($"{Player.allowedinput}VerAim");
            horizontalInput = Input.GetAxis($"{Player.allowedinput}HorAim");
            DirectionInput = new Vector3(FacingLeft ? -horizontalInput : horizontalInput, verticalInput);
            if (DirectionInput.sqrMagnitude > 0.1f)
            {
                if (FacingLeft ? horizontalInput < -0.1f : horizontalInput > 0.1f)
                {
                    Direction = ((transform.position + DirectionInput) - transform.position);
                    float MinClamp = -90;
                    float MaxClamp = 90;
                    float angle = Mathf.Clamp(Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg, MinClamp, MaxClamp);
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
                }
            }
        }
    }
}
