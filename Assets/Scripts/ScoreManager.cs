using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    class Char
    {
        public Character character;
        public Vector3 startscale;
        public float charheight;
    }
    List<Char> character_list = new List<Char>();
    private void Start()
    {
        FillCharList();
        StartCoroutine(PlayerScale());
    }
    void FillCharList()
    {
        foreach (Character Char_ in FindObjectsOfType<Character>())
        {
            Char temp = new Char()
            {
                character = Char_,
                startscale = Char_.transform.localScale,
                charheight = Char_.GetComponent<BoxCollider2D>().size.y
            };
            character_list.Add(temp);
        }
    }
    IEnumerator PlayerScale()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            for (int i = 0; i < character_list.Count; i++)
            {
                Vector3 CharScale = character_list[i].character.transform.localScale;
                int playerScore = GameManager.Playerscores[character_list[i].character.playerNR];
                Vector3 sizeToScale = character_list[i].startscale + new Vector3(playerScore * 2, playerScore * 2);

                while (sizeToScale.magnitude > character_list[i].character.transform.localScale.magnitude)
                {
                    character_list[i].character.transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2);
                    yield return new WaitForSeconds(Time.deltaTime);
                }

            }
            yield return new WaitForSeconds(0);
        }
    }
}
