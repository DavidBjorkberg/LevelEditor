using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Explode : MonoBehaviour
{
   
    List<GameObject> NearbyObjects = new List<GameObject>();
    public List<string> HittableTags;
    internal GameObject player;
    internal float ExplosionRadius;
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = ExplosionRadius;
        
        Invoke("ExplodeFunc", 0.1f);
        if (player == null)
        {
            player = new GameObject();
        }
    }
    private void Update()
    {
        if (NearbyObjects.Count > 0)
        {
            CancelInvoke();
            ExplodeFunc();
        }

    }
    void ExplodeFunc()
    {
        for (int i = 0; i < NearbyObjects.Count; i++)
        {
            NearbyObjects[i].SendMessage("Explode", SendMessageOptions.DontRequireReceiver);
            
            switch (NearbyObjects[i].tag)
            {
                case "Player":
                    Main.KillPlayer(NearbyObjects[i].gameObject, player);                
                    break;
                case "Box":
                    NearbyObjects[i].GetComponent<BoxScript>().Destroyed();
                    break;
                case "Destructible":
                    Destroy(NearbyObjects[i]);
                    break;
                default:
                    break;
            }
        }
        NearbyObjects.Clear();
        GetComponent<Animator>().Play("Explosion");
        Destroy(gameObject, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NearbyObjects.Add(collision.gameObject);
    }
}
