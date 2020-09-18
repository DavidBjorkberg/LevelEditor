using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class AttachObjects : MonoBehaviour
{

    public List<GameObject> attachedObjects = new List<GameObject>();
    private void Update()
    {
        Collider2D[] attachCheck = Physics2D.OverlapBoxAll(new Vector3(transform.position.x, transform.position.y + 0.1f)
            , new Vector3(GetComponent<SpriteRenderer>().bounds.size.x - 0.05f
            , GetComponent<SpriteRenderer>().bounds.size.y), 0);
        //Add objects to attachedObjects array that are sitting on the object
        //Only attach objects when in editor mode.
        if (!Application.isPlaying)
        {
            AttachHitObjects(attachCheck);
        }
        //Remove objects from attached list that has been destroyed or fallen off
        if (GetComponent<SpriteRenderer>().enabled)
        {
            DetachMissingObjects(attachCheck);
        }
    }
    void AttachHitObjects(Collider2D[] attachCheck)
    {
        foreach (Collider2D hit in attachCheck)
        {
            bool add = true;
            for (int i = 0; i < attachedObjects.Count; i++)
            {
                if (attachedObjects[i] == hit.gameObject)
                {
                    add = false;
                }
            }
            if (add && hit.gameObject != gameObject)
            {
                attachedObjects.Add(hit.gameObject);
            }
        }
        EditorUtility.SetDirty(this);
    }
    void DetachMissingObjects(Collider2D[] attachCheck)
    {
        foreach (GameObject attachedObject in attachedObjects)
        {
            if (attachedObject == null)
            {
                attachedObjects.Remove(attachedObject);
                break;
            }
            else if (!Contains(attachCheck, attachedObject))
            {
                attachedObjects.Remove(attachedObject);
                break;
            }
        }
    }
    bool Contains(Collider2D[] array, GameObject target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if(array[i].gameObject == target)
            {
                return true;
            }
        }
        return false;
    }

}

