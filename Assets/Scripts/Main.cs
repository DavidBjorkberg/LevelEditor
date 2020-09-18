using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static GameObject Explosion;
    private void Start()
    {
        Explosion = FindObjectOfType<GameManager>().Explosion;
    }
    public static void KillPlayer(GameObject Killed, GameObject Killer)
    {
        Killed?.GetComponent<Character>().Died();
        Killer?.GetComponent<Character>().OnGetKill();
    }
    public static void DrawBox(Vector3 CenterOfBox, Vector3 Size)
    {
        Vector3 DownLeftCorner = CenterOfBox + new Vector3(-Size.x / 2, -Size.y / 2);
        Vector3 DownRightCorner = CenterOfBox + new Vector3(Size.x / 2, -Size.y / 2);
        Vector3 UpLeftCorner = CenterOfBox + new Vector3(-Size.x / 2, Size.y / 2);
        Vector3 UpRightCorner = CenterOfBox + new Vector3(Size.x / 2, Size.y / 2);
        Debug.DrawLine(DownLeftCorner, DownRightCorner, Color.green, 1);
        Debug.DrawLine(DownRightCorner, UpRightCorner, Color.green, 1);
        Debug.DrawLine(UpRightCorner, UpLeftCorner, Color.green, 1);
        Debug.DrawLine(UpLeftCorner, DownLeftCorner, Color.green, 1);
    }
    public static void GivePoints(Character character, int Amount)
    {
        GameManager.Playerscores[character.playerNR] += Amount;
        switch (Amount)
        {
            case 1:
                character.transform.Find("+1Point").gameObject.SetActive(true);
                break;
            case 2:
                character.transform.Find("+2Point").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public static void SpawnExplosion(Vector3 ExplosionPos, float ExplosionRadius, GameObject player)
    {
        GameObject explosion = Instantiate(Explosion, ExplosionPos, Quaternion.identity);
        explosion.GetComponent<Explode>().player = player;
        explosion.GetComponent<Explode>().ExplosionRadius = ExplosionRadius;
    }
    public static void SpawnExplosion(Vector3 ExplosionPos, float ExplosionRadius)
    {
        GameObject explosion = Instantiate(Explosion, ExplosionPos, Quaternion.identity);
        explosion.GetComponent<Explode>().ExplosionRadius = ExplosionRadius;
    }

}
