using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : PlayerBullet
{
    public override void Start()
    {
        base.Start();
        rb.velocity = Direction * gunstats.bulletspeed;
    }

    public override void Update()
    {
        float distance = Vector3.Distance(Startpos, transform.position);
        if (distance >= gunstats.Range)
        {
            Destroy(gameObject);
        }
        Quaternion toRotation = Quaternion.FromToRotation(Vector3.right, GetComponent<Rigidbody2D>().velocity);
        transform.rotation = toRotation;
    }
}
