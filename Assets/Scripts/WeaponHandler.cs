using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandler : MonoBehaviour
{
    internal GameObject EquippedGun;
    internal WeaponStats weaponstats;
    internal Vector3 direction;
    internal Collider2D Coll2D;
    internal Weapon weapon;
    string allowedinput;
    internal bool CanPickup = true;
    internal bool UsingWeapon;
    void Start()
    {
        allowedinput = GetComponentInParent<Character>().allowedinput;
        if (allowedinput == null)
        {
            allowedinput = "Keyboard1";
        }
    }

    void Update()
    {
        if (EquippedGun != null)
        {
            weaponstats.Firerate += Time.deltaTime;
            direction = GetComponentInParent<Character>().direction;
            if (Input.GetButtonDown(allowedinput + "Shoot"))
            {
                if (!weapon.firing && weaponstats.Firerate > weaponstats.StartFirerate)
                {
                    StartCoroutine(weapon.StartShooting());
                }
            }
            if (Input.GetButtonUp(allowedinput + "Shoot"))
            {
                weapon.StopShooting();
            }
        }

    }

    public void EquipWeapon(GameObject Weapon)
    {
        EquippedGun = Weapon;
        weapon = EquippedGun.GetComponent<Weapon>();
        weaponstats = EquippedGun.GetComponent<WeaponStats>();
        EquippedGun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Coll2D = EquippedGun.GetComponent<Collider2D>();
        EquippedGun.transform.rotation = GetComponent<Character>().direction == Vector3.right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        CanPickup = false;
        Invoke("PickupTrue", 0.3f);
        Coll2D.enabled = false;
        EquippedGun.transform.parent = transform.GetChild(1);
        EquippedGun.transform.position = transform.GetChild(1).transform.position;
        EquippedGun.GetComponent<Weapon>().PickedUp();
    }

    public void DropWeapon()
    {
        EquippedGun.transform.parent = null;
        weapon = null;
        EquippedGun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        EquippedGun = null;
        Coll2D.enabled = true;
    }
    void PickupTrue()
    {
        CanPickup = true;
    }
}
