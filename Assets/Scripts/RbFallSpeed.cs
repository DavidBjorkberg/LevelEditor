using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class RbFallSpeed : MonoBehaviour
{
    Rigidbody2D rb;
    float MaxSpeed = 30;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
        }

    }
}
