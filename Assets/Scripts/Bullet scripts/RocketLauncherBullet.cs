using UnityEngine;

public class RocketLauncherBullet : PlayerBullet
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Main.SpawnExplosion(transform.position, gunstats.ExplosionRadius, shooter.gameObject);
        Destroy(gameObject);
    }
   
}
