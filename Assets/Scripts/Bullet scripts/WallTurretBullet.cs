using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurretBullet : MonoBehaviour
{
    Rigidbody2D rb;
    internal float bulletspeed;
    internal Vector3 Direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Direction * bulletspeed;
        Destroy(gameObject, 5);
    }
    private void Update()
    {
        Quaternion toRotation = Quaternion.FromToRotation(Vector3.right, GetComponent<Rigidbody2D>().velocity);
        transform.rotation = toRotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag.Contains("Player"))
        {
            collision.collider.GetComponent<Character>().Died();
        }
        Destroy(gameObject);
    }

}
