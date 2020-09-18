using UnityEngine;

public class AppearingGround : MonoBehaviour
{
    public float appearTime;
    SpriteRenderer mesh;
    BoxCollider2D box2D;
    AttachObjects attach;
    private void Start()
    {
        attach = GetComponent<AttachObjects>();
        mesh = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();
        mesh.enabled = false;
        box2D.enabled = false;
        foreach (var attachedObject in attach.attachedObjects)
        {
            attachedObject.SetActive(false);
        }
    }
    public void Activate(float AliveTime)
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
        if (AliveTime != 0)
        {
            Invoke("Deactivate", AliveTime);
        }
    }
    void Deactivate()
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
    }
    void Flicker()
    {
        mesh.enabled = !mesh.enabled;
    }
}

