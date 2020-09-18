using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawLinesPortal : MonoBehaviour
{
    public List<Portal> Linked = new List<Portal>();
    DrawLines drawlines;
    Animator animator;
    SpriteRenderer spriterenderer;
    public List<Sprite> PortalSprites;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        drawlines = FindObjectOfType<DrawLines>();

    }
    void Update()
    {
        if (drawlines.DirectionLines)
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                if (GetComponent<Portal>().channel == portal.channel && portal != GetComponent<Portal>() && !Linked.Contains(portal))
                {
                    Linked.Add(portal);
                }
                else if (GetComponent<Portal>().channel != portal.channel && Linked.Contains(portal))
                {
                    Linked.Remove(portal);
                }
            }
            if (Linked.Count > 0)
            {
                foreach (Portal linked in Linked)
                {

                    if (linked != null)
                    {
                        Debug.DrawLine(transform.position, linked.transform.position, Color.cyan);                
                    }
                }
            }
        }
        switch (GetComponent<Portal>().channel)
        {
            case Portal.PortalChannels.A:
                animator.Play("GreenPortalAnim");
                spriterenderer.sprite = PortalSprites[0];
                break;
            case Portal.PortalChannels.B:
                animator.Play("OrangePortalAnim");
                spriterenderer.sprite = PortalSprites[1];
                break;
            case Portal.PortalChannels.C:
                animator.Play("DarkbluePortalAnim");
                spriterenderer.sprite = PortalSprites[2];
                break;
            case Portal.PortalChannels.D:
                animator.Play("TealPortalAnim");
                spriterenderer.sprite = PortalSprites[3];
                break;
            case Portal.PortalChannels.E:
                animator.Play("PinkPortalAnim");
                spriterenderer.sprite = PortalSprites[4];
                break;
            case Portal.PortalChannels.F:
                animator.Play("RedPortalAnim");
                spriterenderer.sprite = PortalSprites[5];
                break;
            default:
                break;
        }

    }
}
