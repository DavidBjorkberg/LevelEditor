using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static int Playeramount;
    internal static int Deadplayers;
    internal static int[] Playerscores = new int[4];
    public static Character[] characters = new Character[Menu.Playeramount];
    public static List<int> SceneList = new List<int>();
    public bool ScreenLeaveDeath;
    public GameObject Explosion;
    //Score u get when you win a round
    int WinScoregain = 2;
    void Start()
    {
        Playeramount = CharacterSelect.Connectedplayers.Count;
    }

    void Update()
    {
        if (Deadplayers >= Playeramount - 1 && Playeramount > 1)
        {
            Invoke("EndRound", 3);
        }
    }
    void EndRound()
    {
        GivePoints();
        LoadNextScene();
        Deadplayers = 0;
    }
    void GivePoints()
    {
        int PlayerWon = GetWinner();
        if (PlayerWon != 5)
        {
            Main.GivePoints(characters[PlayerWon], WinScoregain);
        }
    }
    void LoadNextScene()
    {
        if (SceneList.Count == 0)
        {
            SceneManager.LoadScene("ScoreScene");
        }
        else
        {
            int scene = GetNextScene();
            SceneList.Remove(scene);
            SceneManager.LoadScene(scene);
        }
    }
    int GetNextScene()
    {
        return SceneList[Random.Range(0, SceneList.Count)];
    }
    int GetWinner()
    {
        int WinNR = 5;
        foreach (Character player in FindObjectsOfType<Character>())
        {
            if (player.gameObject.activeSelf)
            {
                WinNR = player.playerNR;
            }
        }
        return WinNR;

    }
}
