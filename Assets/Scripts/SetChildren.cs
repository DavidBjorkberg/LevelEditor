using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class SetChildren : MonoBehaviour
{
    [SerializeField, Header("Set these to child on game start")]
    public List<Transform> Children;
    void Update()
    {
        if (!Application.isPlaying)
        {
            SetAttachedObjectsAsChild();
        }
    }
    void SetAttachedObjectsAsChild()
    {
        Collider2D[] AttachCheck = Physics2D.OverlapBoxAll(new Vector3(transform.position.x
            , transform.position.y + 0.1f)
    , new Vector3(GetComponent<SpriteRenderer>().bounds.size.x - 0.05f
    , GetComponent<SpriteRenderer>().bounds.size.y), 0);

        foreach (Collider2D Coll in AttachCheck)
        {
            if (Coll.gameObject != gameObject && Coll.transform.parent != gameObject 
                && !Children.Contains(Coll.transform) && Coll.name != "Bottom")
            {
                Children.Add(Coll.transform);
            }
            EditorUtility.SetDirty(this);
        }
        List<GameObject> attachCheck = new List<GameObject>();
        foreach (Collider2D Hit in AttachCheck)
        {
            attachCheck.Add(Hit.gameObject);
        }
        foreach (Transform Attached in Children)
        {
            if (Attached == null)
            {
                Children.Remove(Attached);
                break;
            }
            else if (!attachCheck.Contains(Attached.gameObject))
            {
                Children.Remove(Attached);
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.activeSelf)
        {
            collision.transform.SetParent(null);
        }
    }
}
