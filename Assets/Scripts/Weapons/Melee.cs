using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : Weapon
{
    public override IEnumerator StartShooting()
    {
        firing = true;
        while (firing)
        {
            yield return new WaitUntil(() => stats.Firerate > stats.StartFirerate);
            if (!firing)
            {
                break;
            }
            stats.Firerate = 0;
            SpriteRenderer sprite = Player.GetComponent<SpriteRenderer>();
            Vector3 CenterOfPlayer = sprite.bounds.center;
            Vector3 QuarterOfHeight = new Vector3(0, sprite.size.y / 4);
            Vector3 EdgeOfPlayer = CenterOfPlayer + new Vector3(Player.direction == Vector3.left ? -sprite.bounds.size.x / 2 : sprite.bounds.size.x / 2, 0);
            Vector3 Offset = new Vector3(stats.Range / 2, 0);
            Vector3 CenterOfBox = EdgeOfPlayer + QuarterOfHeight + Offset;
            Vector3 Size = new Vector3(stats.Range, sprite.size.y);
            Collider2D[] HitObjects = Physics2D.OverlapBoxAll(CenterOfBox, Size, 0, 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14);
            Main.DrawBox(CenterOfBox, Size);
            foreach (Collider2D Hit in HitObjects)
            {
                if (Hit.gameObject == Player.gameObject)
                {
                    continue;
                }
                Main.KillPlayer(Hit.gameObject, Player.gameObject);
            }
        }
    }
}
