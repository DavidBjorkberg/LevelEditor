using System.Collections.Generic;
using UnityEngine;


public class DisappearingGround : MonoBehaviour
{   
    public float DisappearTime;
    SpriteRenderer mesh;
    BoxCollider2D box2D;
    AttachObjects attach;
    private void Start()
    {
        attach = GetComponent<AttachObjects>();
        mesh = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();
    }
    public void Activate(float AliveTime)
    {
        if (GetComponent<SetChildren>() != null)
        {
            GetComponent<SetChildren>().enabled = false;
        }
        foreach (var attachedObject in attach.attachedObjects)
        {
            attachedObject.SetActive(false);
        }
        mesh.enabled = false;
        box2D.enabled = false;
        Invoke("Deactivate", AliveTime);
    }
    void Deactivate()
    {
        foreach (var attachedObject in attach.attachedObjects)
        {
                attachedObject.SetActive(true);
        }
        if (GetComponent<SetChildren>() != null)
        {
            GetComponent<SetChildren>().enabled = true;
        }
        mesh.enabled = true;
        box2D.enabled = true;
    }
}
