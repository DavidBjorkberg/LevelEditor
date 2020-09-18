using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int Playeramount;
    public Dropdown mydropdown;

    public void Nextscene()
    {
        Playeramount = mydropdown.value;
        if (Playeramount > 0)
        {
            SceneManager.LoadScene("Character select");
        }
        else
        {
            print("Playeramount < 1");
        }
    }
  
}
