using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    public int movementSpeed;
    public int jumpHeight;
    Rigidbody2D rb;
    internal string allowedinput;
    GameObject pickuptext;
    WeaponHandler shoot;
    internal Vector3 direction;
    internal GameObject bottom;
    internal int playerNR;
    internal bool hasControl = true;
    public UnityEvent Onkill;
    float hitboxWidth;
    LadderMovement laddermovement;
    internal float height;
    internal bool hasGunEquipped;

    void Start()
    {
        direction = Vector3.right;
        if (allowedinput == null)
        {
            allowedinput = "Keyboard1";
        }
        pickuptext = transform.GetChild(0).gameObject;
        pickuptext.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        shoot = GetComponent<WeaponHandler>();
        hitboxWidth = GetComponent<BoxCollider2D>().size.x;
        bottom = transform.Find("Bottom").gameObject;
        laddermovement = GetComponent<LadderMovement>();
    }
    private void Update()
    {
        if (hasControl)
        {
            if (!laddermovement.Onladder)
            {
                Movement();
                PickUp();
                Kick();
                Jump();
                Ability();
            }
        }
        hasGunEquipped = shoot.EquippedGun != null;
    }
    protected abstract void Ability();
    protected void Movement()
    {
        RaycastHit2D WalkCheck = Physics2D.BoxCast(transform.position, new Vector2(0.25f, 1), 0, direction, 0.3f, 1 << 8 | 1 << 21 | 1 << 20);
        //Right
        if (Input.GetAxis($"{allowedinput}Hor") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (!WalkCheck)
            {
                transform.Translate(Vector2.right * Input.GetAxis($"{allowedinput}Hor") * Time.deltaTime * movementSpeed);
            }
            if (hasGunEquipped && direction != Vector3.right)
            {
                shoot.EquippedGun.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            direction = Vector3.right;
        }
        //Left
        if (Input.GetAxis($"{allowedinput}Hor") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            if (!WalkCheck)
            {
                transform.Translate(Vector2.right * Input.GetAxis($"{allowedinput}Hor") * Time.deltaTime * movementSpeed);
            }
            if (hasGunEquipped && direction != Vector3.left)
            {
                shoot.EquippedGun.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            direction = Vector3.left;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown($"{allowedinput}Jump"))
        {
            RaycastHit2D GroundedCheck;                                                                                           
            GroundedCheck = Physics2D.BoxCast(bottom.transform.position
                , new Vector2(hitboxWidth, 0.1f)
                , 0
                , Vector2.down
                , 0.2f
                , 1 << 8 | 1 << 19 | 1 << 23); // Floor, box and button layer

            if (GroundedCheck)
            {
                rb.AddForce(Vector2.up * jumpHeight);
            }
        }
    }
    void Kick()
    {
        RaycastHit2D KickCheck;
        KickCheck = Physics2D.Raycast(bottom.transform.position, direction, 1, 1 << 19);
        if (KickCheck)
        {
            if (Input.GetButtonDown($"{allowedinput}Kick"))
            {
                KickCheck.collider.GetComponent<BoxScript>().Destroyed();
            }
        }

    }
    void PickUp()
    {
        RaycastHit2D PickupCheck = Physics2D.BoxCast(bottom.transform.position
            , new Vector2(hitboxWidth, 0.25f)
            , 0
            , Vector2.down
            , 0.1f
            , 1 << 10);
        pickuptext.SetActive(PickupCheck);
        if (PickupCheck)
        {
            pickuptext.GetComponent<TextMesh>().text 
                = $"{PickupCheck.collider.GetComponent<Weapon>().GetName()} (X)";
            if (Input.GetButtonDown($"{allowedinput}Pickup") && shoot.CanPickup)
            {
                if (hasGunEquipped)
                {
                    shoot.DropWeapon();
                }
                shoot.EquipWeapon(PickupCheck.collider.gameObject);
            }
        }
    }
    public void Died()
    {
        GameManager.Deadplayers++;
        gameObject.SetActive(false);
    }
    public void OnGetKill()
    {
        int Killscore = 1;
        Main.GivePoints(this, Killscore);
    }
}
