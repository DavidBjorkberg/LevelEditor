using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racecar : MonoBehaviour
{
    internal string allowedinput;
    internal RobotDude player;
    Rigidbody2D rb;
    public GameObject Explosion;
    float HitboxWidth;
    float movementSpeed;
    void Start()
    {
        Invoke("Explode", player.ExplodeTimer);
        rb = GetComponent<Rigidbody2D>();
        HitboxWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        movementSpeed = player.CarSpeed;
    }

    void Update()
    {
        Move();
        Fly();
        if (Input.GetButtonDown(allowedinput + "Ability"))
        {
            Explode();
        }
    }
    void Move()
    {
        movementSpeed += player.MovementSpeedGrowth * Time.deltaTime;
        transform.Translate(Vector2.right * Input.GetAxis(allowedinput + "Hor") * Time.deltaTime * movementSpeed);
        GetComponent<SpriteRenderer>().flipX = Input.GetAxis(allowedinput + "Hor") < 0;
    }
    void Fly()
    {
        RaycastHit2D UpCheck = Physics2D.BoxCast(transform.position
            , new Vector2(HitboxWidth, 0.25f)
            , 0
            , Vector2.up
            , 0.2f, 1 << 8);
        if (Input.GetButton(allowedinput + "Jump") && !UpCheck)
        {
            rb.velocity = Vector2.zero;
            transform.Translate(Vector2.up * Time.deltaTime * movementSpeed);
            rb.gravityScale = 0;
        }

        if (Input.GetButtonUp(allowedinput + "Jump"))
        {
            rb.gravityScale = 1;
        }
    }
    void Explode()
    {
        Main.SpawnExplosion(transform.position, player.ExplosionRadius, player.gameObject);
        player.GetComponent<Character>().hasControl = true;
        Destroy(gameObject);
    }
}
