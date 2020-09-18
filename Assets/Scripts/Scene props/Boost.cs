using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [Range(5, 20)] public float BoostHeight;
    private BoxCollider2D hitbox;
    Animator animator;
    private void Start()
    {
        hitbox = GetComponentInChildren<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Vector3 hitboxSize = hitbox.size * transform.localScale;
     Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position
         , hitboxSize
         , transform.eulerAngles.y
         , ~(1 << 8));
        for (int i = 0; i < hits.Length; i++)
        {
            BoostObject(hits[i]);
        }
    }
    void BoostObject(Collider2D boostObject)
    {
        if (!boostObject.isTrigger && boostObject.GetComponent<Rigidbody2D>() != null)
        {
            animator.Play("Activate");
            boostObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
     boostObject.GetComponent<Rigidbody2D>().AddForce(transform.up * BoostHeight, ForceMode2D.Impulse);
        }
    }
}
