using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDoor : MonoBehaviour
{
    public enum Opendirection
    {
        Left, Right
    }
    bool open;
    SpriteRenderer Renderer;
    public Opendirection OpenDirection;
    public float OpenTime;
    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    public void OpenFromLeft()
    {
        Renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        open = true;
    }
    public void OpenFromRight()
    {
        Renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        open = true;
    }
    void CloseDoor()
    {
        Renderer.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        open = false;
    }
    public void OnButtonClick(float OpenTime)
    {
        if (!open)
        {
            if (OpenDirection == Opendirection.Right)
            {
                OpenFromLeft();
            }
            else
            {
                OpenFromRight();
            }
            Invoke("CloseDoor", OpenTime);
        }
        else
        {
            CloseDoor();
        }
    }
}
