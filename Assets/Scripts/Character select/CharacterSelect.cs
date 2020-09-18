using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public static List<string> Connectedplayers = new List<string>();
    public static string[] Names = new string[Menu.Playeramount];
    public static string[] allowedinput = new string[Menu.Playeramount];
    public static List<string> ReadyPlayers = new List<string>();
    public GameObject Characterpicker;
    string controllerInput = "Controller";
    string keyBoardInput = "Keyboard";
    bool KeyboardConnected, Controller1Connected, Controller2Connected, Controller3Connected, Controller4Connected;
    public GameObject Startgametext;
    int PlayerNR;
    void Start()
    {
        if (GameManager.SceneList.Count == 0)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneUtility.GetScenePathByBuildIndex(i).Contains("Level"))
                {
                    GameManager.SceneList.Add(i);
                }
            }
        }
        Startgametext.SetActive(false);
    }
    void Update()
    {
        PlayerConnect();
        Startgametext.SetActive(ReadyPlayers.Count == Connectedplayers.Count && Connectedplayers.Count > 0 ? true : false);
    }
    void PlayerConnect()
    {
        string playername = $"Player{PlayerNR + 1}";
        if (Input.GetButtonDown("Keyboard1Jump") && !KeyboardConnected)
        {
            KeyboardConnected = Connect(playername, keyBoardInput);
        }
        if (Input.GetButtonDown("Controller1Jump") && !Controller1Connected)
        {
            Controller1Connected = Connect(playername, controllerInput);
        }
        if (Input.GetButtonDown("Controller2Jump") && !Controller2Connected)
        {
            Controller2Connected = Connect(playername, controllerInput);
        }
        if (Input.GetButtonDown("Controller3Jump") && !Controller3Connected)
        {
            Controller3Connected = Connect(playername, controllerInput);
        }
        if (Input.GetButtonDown("Controller4Jump") && !Controller4Connected)
        {
            Controller4Connected = Connect(playername, controllerInput);
        }
    }
    bool Connect(string playername, string Input)
    {
        Names[Connectedplayers.Count] = playername;
        allowedinput[Connectedplayers.Count] = $"{Input}1";

        GameObject spawned = Instantiate(Characterpicker, Vector3.zero, Quaternion.identity);
        spawned.name = playername;
        spawned.GetComponent<character_picker>().PlayerNR = PlayerNR;
        spawned.GetComponent<character_picker>().allowedinput = allowedinput[Connectedplayers.Count];

        Connectedplayers.Add(playername);
        PlayerNR++;
        return true;
    }

}
