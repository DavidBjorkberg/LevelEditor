using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ThrowableObject : Weapon
{
    protected float ThrowCharge;
    protected Image ThrowBorder;
    protected Vector3 Direction;
    protected Vector3 DirectionInput;
    protected float ThrowStrength;
    //Charge the throw
    public override IEnumerator StartShooting()
    {
        GameObject Canvas = transform.root.Find("Canvas").gameObject;
        ThrowBorder = Canvas.transform.Find("ThrowBorder").GetComponent<Image>();
        Image ThrowBar = ThrowBorder.transform.Find("ThrowBar").GetComponent<Image>();
        GameObject Rotater = ThrowBorder.transform.Find("ThrowArrowRotater").gameObject;

        Player.hasControl = false;
        Direction = Player.direction;  
        ThrowBar.fillAmount = 0;
        ThrowCharge = 0;
        firing = true;
        while (firing)
        {
            Rotate(Rotater);
            ThrowCharge = Mathf.Clamp(ThrowCharge + Time.deltaTime / 2.5f, 0, 1);
            float ThrowMultiplier = 2;
            float MinimumStrength = 0.1f;
            ThrowStrength = MinimumStrength + (ThrowCharge * ThrowMultiplier);
            ThrowBorder.gameObject.SetActive(true);
            ThrowBar.fillAmount = ThrowCharge;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.3f);
        Player.hasControl = true;
    }
    public override void StopShooting()
    {
        Direction *= ThrowStrength;
        ThrowObject();
        firing = false;
    }
    void Rotate(GameObject Rotater)
    {
        if (Player.allowedinput.Contains("Keyboard"))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos = new Vector3(MousePos.x, MousePos.y, 0);
            Direction = (MousePos - Rotater.transform.position).normalized;
        }
        else
        {
            DirectionInput = new Vector3(Input.GetAxis($"{Player.allowedinput}Hor")
                , Input.GetAxis($"{Player.allowedinput}Ver"));
            if (DirectionInput.sqrMagnitude > 0.1f)
            {
                Direction = ((Rotater.transform.position + DirectionInput) - Rotater.transform.position).normalized;
            }
        }
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, Direction);
        Rotater.transform.rotation = rotation;
    }
    public virtual void ThrowObject()
    {
        ThrowBorder.gameObject.SetActive(false);
        GameObject ThrownObject = Instantiate(stats.Bullet, transform.position, Quaternion.identity);
        BulletShot(ThrownObject);
        StartCoroutine(DestroyObject());
        
    }  
    IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        weaponhandler.DropWeapon();
        Destroy(gameObject);
    }
    public override void PickedUp()
    {
        Player = GetComponentInParent<Character>();
        stats = GetComponent<WeaponStats>();
        weaponhandler = Player.GetComponent<WeaponHandler>();
    }
}
