using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    internal Vector3 Gunpoint;
    internal GameObject Muzzleflash;
    GameObject Player;
    WeaponStats stats;
    WeaponHandler weaponhandler;
    internal bool firing;
    private void Update()
    {
        if (transform.parent != null)
        {
            if (transform.root.tag.Contains("Player"))
            {
                Player = transform.root.gameObject;
                stats = GetComponent<WeaponStats>();
                weaponhandler = Player.GetComponent<WeaponHandler>();
                if (weaponhandler.weaponstats.WeaponType != WeaponStats.Weapon_type.Throw)
                {
                    Gunpoint = GetComponent<WeaponStats>().gameObject.transform.GetChild(0).position;
                    Muzzleflash = transform.GetChild(1).gameObject;
                    Muzzleflash.SetActive(false);
                }
            }
        }
    }
    public IEnumerator Rifle()
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

            GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
            shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;

        }
    }
    public void StopRifle()
    {
        firing = false;
    }
    public IEnumerator Shotgun()
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
                GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
                shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;
                shotbullet.GetComponent<ShotgunBullet>().Direction = weaponhandler.direction + new Vector3(0, StartAngle - Anglechange, 0);
                StartAngle -= Anglechange;
            }

        }
    }
    public void StopShotgun()
    {
        firing = false;
    }
    public IEnumerator GrenadeLauncher()
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

            GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
            shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;
            shotbullet.GetComponent<GrenadeBullet>().Direction = weaponhandler.direction + new Vector3(0, stats.ShotAngle, 0);

        }
    }
    public void StopGrenadeLauncher()
    {
        firing = false;
    }
    public IEnumerator RocketLauncher()
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

            GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
            shotbullet.GetComponent<PlayerBullet>().shooter = weaponhandler;
        }
    }
    public void StopRocketLauncher()
    {
        firing = false;
    }
    public IEnumerator RayGun()
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
            RaycastHit2D HitCheck = Physics2D.Raycast(Gunpoint, weaponhandler.direction, 40, ~(transform.root.gameObject.layer | 1 << 15 | 1 << 16 | 1 << 17 | 1 << 18 | 1 << 22 | 1 << 25 | 1 << 0));
            float Distance;
            if (HitCheck)
            {
                Distance = HitCheck.distance;
                if (HitCheck.collider.tag.Contains("Player"))
                {
                    Main.KillPlayer(HitCheck.collider.gameObject, Player);
                    Distance += HitCheck.collider.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                }
            }
            else
                Distance = weaponhandler.direction == Vector3.right ? 40 : -40;
            GameObject Ray = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
            Ray.GetComponent<RayScript>().HitPos = HitCheck.point;
            Ray.GetComponent<RayScript>().Distance = Distance;
            Ray.SendMessage("SetShooter", weaponhandler, SendMessageOptions.RequireReceiver);
        }
    }
    public void StopRayGun()
    {
        firing = false;
    }
    //Spawna spheres
    //spheres lägger på kraft åt motsatt håll
    //Partiklar följer spheren
    public void Watergun()
    {
        //GameObject shotbullet = Instantiate(stats.Bullet, Gunpoint, Quaternion.identity);
    }
    public IEnumerator Melee()
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
            SpriteRenderer sprite = Player.GetComponent<SpriteRenderer>();
            Vector3 CenterOfPlayer = sprite.bounds.center;
            Vector3 QuarterOfHeight = new Vector3(0, sprite.size.y / 4);
            Vector3 EdgeOfPlayer = CenterOfPlayer + new Vector3(sprite.bounds.size.x / 2, 0);
            Vector3 Offset = new Vector3(stats.Range / 2, 0);
            Vector3 CenterOfBox = EdgeOfPlayer + QuarterOfHeight + Offset;
            Vector3 Size = new Vector3(stats.Range, sprite.size.y);
            Collider2D[] HitObjects = Physics2D.OverlapBoxAll(CenterOfBox, Size, 0, 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14);
            foreach (Collider2D Hit in HitObjects)
            {
                if (Hit.gameObject == Player)
                {
                    continue;
                }
                Main.KillPlayer(Hit.gameObject, Player);
            }
        }
    }
    public void StopMelee()
    {
        firing = false;
    }
    float ThrowCharge;
    Image ThrowBorder;
    Vector3 Direction;
    Vector3 DirectionInput;
    float ThrowStrength;
    //Here u Prepare the throw 
    public IEnumerator ChargeThrow()
    {
        GameObject Canvas = transform.root.Find("Canvas").gameObject;
        ThrowBorder = Canvas.transform.Find("ThrowBorder").GetComponent<Image>();
        Image ThrowBar = ThrowBorder.transform.Find("ThrowBar").GetComponent<Image>();
        GameObject Rotater = ThrowBorder.transform.Find("ThrowArrowRotater").gameObject;

        Player.GetComponent<Character>().hasControl = false;
        Direction = Player.GetComponent<Character>().direction;
        ThrowBar.fillAmount = 0;
        ThrowCharge = 0;
        firing = true;
        while (firing)
        {
            Rotate(Rotater);
            ThrowCharge = Mathf.Clamp(ThrowCharge + Time.deltaTime / 2.5f, 0, 1);
            float ThrowMultiplier = 2f;
            float MinimumStrength = 0.1f;
            ThrowStrength = MinimumStrength + (ThrowCharge * ThrowMultiplier);
            ThrowBorder.gameObject.SetActive(true);
            ThrowBar.fillAmount = ThrowCharge;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Character>().hasControl = true;
    }
    public void StopThrow()
    {
        Throw(Direction * ThrowStrength);
        firing = false;
    }
    void Rotate(GameObject Rotater)
    {
        if (Player.GetComponent<Character>().allowedinput.Contains("Keyboard"))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos = new Vector3(MousePos.x, MousePos.y, 0);
            Direction = (MousePos - Rotater.transform.position).normalized;
        }
        else
        {
            DirectionInput = new Vector3(Input.GetAxis(Player.GetComponent<Character>().allowedinput + "Hor"), Input.GetAxis(Player.GetComponent<Character>().allowedinput + "Ver"));
            if (DirectionInput.sqrMagnitude > 0.1f)
            {
                Direction = ((Rotater.transform.position + DirectionInput) - Rotater.transform.position).normalized;
            }
        }
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, Direction);
        Rotater.transform.rotation = rotation;
    }
    //Here u throw the object
    public void Throw(Vector3 Direction)
    {
        weaponhandler.UsingWeapon = false;
        ThrowBorder.gameObject.SetActive(false);
        GameObject Grenade = Instantiate(stats.Bullet, transform.position, Quaternion.identity);
        Grenade.GetComponent<GrenadeBullet>().Direction = Direction;
        Grenade.GetComponent<GrenadeBullet>().shooter = weaponhandler;
        Grenade.GetComponent<GrenadeBullet>().Thrown = true;
        StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        weaponhandler.DropWeapon();
        Destroy(gameObject);
    }
    void MuzzleflashFunc(float Duration)
    {
        MuzzleflashEnable();
        CancelInvoke("MuzzleflashDisable");
        Invoke("MuzzleflashDisable", Duration);
    }
    void MuzzleflashEnable()
    {
        Muzzleflash.SetActive(true);
    }
    void MuzzleflashDisable()
    {
        Muzzleflash.SetActive(false);
    }

}
