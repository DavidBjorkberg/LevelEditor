using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    internal Transform Gunpoint;
    internal GameObject Muzzleflash;
    protected Character Player;
    protected WeaponStats stats;
    protected WeaponHandler weaponhandler;
    internal bool firing;

    public abstract IEnumerator StartShooting();  
    public virtual void StopShooting()
    {
        firing = false;
    }
    public abstract string GetName();
    public virtual void PickedUp()
    {
        Player = GetComponentInParent<Character>();
        stats = GetComponent<WeaponStats>();
        weaponhandler = Player.GetComponent<WeaponHandler>();
        Gunpoint = GetComponent<WeaponStats>().gameObject.transform.GetChild(0);
        Muzzleflash = transform.GetChild(1).gameObject;
        Muzzleflash.SetActive(false);
    }
    protected virtual void BulletShot(GameObject bullet)
    {
    }
   protected void MuzzleflashFunc(float Duration)
    {
        MuzzleflashEnable();
        CancelInvoke("MuzzleflashDisable");
        Invoke("MuzzleflashDisable", Duration);
    }
    protected void MuzzleflashEnable()
    {
        Muzzleflash.SetActive(true);
    }
    protected void MuzzleflashDisable()
    {
        Muzzleflash.SetActive(false);
    }
}
