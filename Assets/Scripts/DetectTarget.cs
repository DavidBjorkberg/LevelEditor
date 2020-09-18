using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    WallTurret turret;
    List<GameObject> Targets = new List<GameObject>();
    void Start()
    {
        turret = GetComponentInParent<WallTurret>();
    }

    void Update()
    {
        TargetCheck();
    }
    void TargetCheck()
    {
        if (turret.Target == null)
        {
            foreach (GameObject target in Targets)
            {
                Vector3 direction = target.transform.position - turret.transform.position;
                RaycastHit2D HitCheck = Physics2D.Raycast(turret.transform.position, direction, 30, 1 << 8 | 1 << 21 | 1 << 20 | 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14);
                Debug.DrawRay(turret.transform.position, direction * 30);
                if (HitCheck)
                {
                    print(HitCheck.collider.name);
                    if (HitCheck.collider.CompareTag("Player"))
                    {
                        turret.Target = target;
                        break;
                    }

                }
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Targets.Contains(collision.gameObject) && collision.gameObject.CompareTag("Player"))
        {
            Targets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Targets.Contains(collision.gameObject))
        {
            Targets.Remove(collision.gameObject);
        }
        if (collision.gameObject == turret.Target)
        {
            turret.Target = null;
        }
    }
}
