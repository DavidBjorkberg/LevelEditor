using UnityEngine;

public class Door : MonoBehaviour
{
    bool Open;
    SpriteRenderer Renderer;
    private void Start()
    {      
        Renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
                                                                                         //Player layers
        RaycastHit2D DoorCheckLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14);
        RaycastHit2D DoorCheckRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14);

        if (DoorCheckLeft || DoorCheckRight)
        {
            if (DoorCheckLeft && !Open)
            {
                OpenFromLeft();
            }
            if (DoorCheckRight && !Open)
            {
                OpenFromRight();
            }
        }
        else
        {
            if (!Renderer.enabled)
            {
                CloseDoor();
            }
        }
    }
    public void OpenFromLeft()
    {
        Renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        Open = true;
    }

    public void OpenFromRight()
    {
        Renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        Open = true;
    }
    void CloseDoor()
    {
        Renderer.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        Open = false;
    }

}
