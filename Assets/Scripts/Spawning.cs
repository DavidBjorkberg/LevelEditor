using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    Vector3[] spawnpoints = new Vector3[4];
    private void Awake()
    {
        //5 = max amount of players + 1
        for (int i = 0; i < 4; i++)
        {
            spawnpoints[i] = GameObject.Find("Spawnpoint" + (i + 1)).transform.position;
        }
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < Menu.Playeramount; i++)
        {
            GameObject SpawnedChar = Instantiate(GameManager.characters[i].gameObject, spawnpoints[i], Quaternion.identity);
            SpawnedChar.name = CharacterSelect.Names[i];
            SpawnedChar.GetComponent<Character>().allowedinput = CharacterSelect.allowedinput[i];
            SpawnedChar.GetComponent<Character>().playerNR = i;
            SpawnedChar.layer = i + 11;
        }
    }

}
