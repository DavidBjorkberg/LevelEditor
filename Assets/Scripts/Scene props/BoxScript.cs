using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public List<GameObject> DropItems;
    public void Destroyed()
    {
        if (DropItems.Count > 0)
        {
            Instantiate(DropItems[Random.Range(0, DropItems.Count)], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}