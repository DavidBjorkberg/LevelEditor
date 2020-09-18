using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : PlayerBullet
{

    public List<string> ExplodeOnHitTags = new List<string>();
    internal bool Thrown;
    public override void Start()
    {
        base.Start();
        if (Thrown)
        {
            rb.velocity = Direction * 8;
            Invoke("Explode", 4);

        }
        else
        {
            rb.velocity = Direction * gunstats.bulletspeed;
            Invoke("Explode", gunstats.ExplodeTime);
        }
    }
    float refref = 0;
    float refrefref = 0;
    public override void Update()
    {
        if (Thrown)
        {
            float y = Mathf.SmoothDamp(rb.velocity.y, -8, ref refref, 0.5f, 26);
            float x = Mathf.SmoothDamp(rb.velocity.x, 2, ref refrefref, 2, 4);
            rb.velocity = new Vector3(x, y);
        }
        else
        {
            float y = Mathf.SmoothDamp(rb.velocity.y, gunstats.yTarget, ref refref, gunstats.ySmoothtime, gunstats.yMaxSpeed);
            float x = Mathf.SmoothDamp(rb.velocity.x, gunstats.xTarget, ref refrefref, gunstats.xSmoothtime, gunstats.xMaxSpeed);
            rb.velocity = new Vector3(x, y);
        }
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (ExplodeOnHitTags.Contains(collision.gameObject.tag))
        {
            Explode();
            CancelInvoke("Explode");
        }
    }

    void Explode()
    {
        Main.SpawnExplosion(transform.position, Thrown ? 0.75f : gunstats.ExplosionRadius, shooter.gameObject);
        Destroy(gameObject);
    }

}