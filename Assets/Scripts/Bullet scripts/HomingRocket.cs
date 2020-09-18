using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : PlayerBullet
{
    GameObject Target;
    List<int> Layers = new List<int>() { 11, 12, 13, 14 };

    public override void Start()
    {
        Layers.Remove(shooter.gameObject.layer);
        base.Start();
    }
    public override void Update()
    {
        if (Target != null)
        {
            Vector3 Direction = Target.transform.position - transform.position;
            Direction.Normalize();
            Quaternion toRotation = Quaternion.FromToRotation(shooter.direction, Direction);
            transform.rotation = toRotation;
            rb.velocity = Direction * gunstats.bulletspeed;
            if (!Target.gameObject.activeSelf)
            {
                Target = null;
            }
        }
        else
        {
            Quaternion toRotation = Quaternion.FromToRotation(Vector3.right, GetComponent<Rigidbody2D>().velocity);
            transform.rotation = toRotation;
        }


    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Main.SpawnExplosion(transform.position, gunstats.ExplosionRadius, shooter.gameObject);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Layers.Contains(collision.gameObject.layer))
        {
            Target = collision.gameObject;
        }
    }
}
