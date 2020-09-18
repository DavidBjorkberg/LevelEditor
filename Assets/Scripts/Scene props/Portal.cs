using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum PortalChannels
    {
        A, B, C, D, E, F
    }
    public enum Exitdirection
    {
        Standard, Left, Right
    }
    public PortalChannels channel;
    public Exitdirection ExitDirection;
    List<Portal> linked = new List<Portal>();
    List<GameObject> arrived = new List<GameObject>(); // To prevent the teleported object to instantly teleport back
    void Start()
    {
        foreach (var portal in FindObjectsOfType<Portal>())
        {
            if (portal.GetComponent<Portal>().channel == channel && portal != this)
            {
                linked.Add(portal);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!arrived.Contains(collision.gameObject) && !collision.isTrigger && collision.GetComponent<Rigidbody2D>() != null)
        {
            int DestinationPortal = Random.Range(0, linked.Count);
            Vector3 ColVelocity = collision.GetComponent<Rigidbody2D>().velocity;
            if (linked[DestinationPortal].gameObject.activeSelf)
            {
                linked[DestinationPortal].arrived.Add(collision.gameObject);
                collision.GetComponent<Rigidbody2D>().MovePosition(linked[DestinationPortal].transform.position);
                collision.transform.position = linked[DestinationPortal].transform.position;

                switch (linked[DestinationPortal].ExitDirection)
                {
                    case Exitdirection.Standard:
                        break;
                    case Exitdirection.Left:
                        if (Vector3.Dot(ColVelocity, -linked[DestinationPortal].transform.right) < 0)
                            collision.GetComponent<Rigidbody2D>().velocity 
                                = Vector3.Reflect(ColVelocity, -linked[DestinationPortal].transform.right);
                        break;
                    case Exitdirection.Right:
                        if (Vector3.Dot(ColVelocity, linked[DestinationPortal].transform.right) < 0)
                            collision.GetComponent<Rigidbody2D>().velocity 
                                = Vector3.Reflect(ColVelocity, linked[DestinationPortal].transform.right);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        arrived.Remove(collision.gameObject);
    }
}