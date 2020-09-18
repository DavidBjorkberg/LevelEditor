using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    internal WeaponHandler shooter;
    protected Rigidbody2D rb;
    protected Vector3 Startpos;
    protected WeaponStats gunstats;
    internal Vector3 Direction;
   public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Startpos = transform.position;
        gunstats = shooter.EquippedGun.GetComponent<WeaponStats>();
        Direction = shooter.direction == Vector3.left ? new Vector3(-Direction.x, Direction.y) : Direction;
        Direction = Direction == Vector3.zero ? shooter.direction : Direction;
        rb.velocity = Direction * gunstats.bulletspeed;
        switch (shooter.gameObject.layer)
        {
            case 11:
                gameObject.layer = 15;
                break;  
            case 12:
                gameObject.layer = 16;
                break;
            case 13:
                gameObject.layer = 17;
                break;
            case 14:
                gameObject.layer = 18;
                break;
            default:
                break;
        }
    }
    public virtual void Update()
    {
        Quaternion toRotation = Quaternion.FromToRotation(Vector3.right, GetComponent<Rigidbody2D>().velocity);
        transform.rotation = toRotation;
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag.Contains("Player"))
        {
            Main.KillPlayer(collision.collider.gameObject, shooter.gameObject);
        }
        Destroy(gameObject);
    }

    void SetShooter(WeaponHandler Shooter)
    {
        shooter = Shooter;
    }
}
