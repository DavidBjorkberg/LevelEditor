using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Character
{
    public int AbilityCharges;
    public int OnKillRecharge;
    public GameObject teleporter;
    GameObject CurrentTP;
    bool ActiveTP;

    protected override void Ability()
    {
        if (Input.GetButtonDown(allowedinput + "Ability"))
        {
            if (ActiveTP)
            {
                transform.position = CurrentTP.transform.position + new Vector3(0, GetComponent<Character>().height * 9);
                Destroy(CurrentTP);
                ActiveTP = false;
            }
            else if (AbilityCharges > 0)
            {
                CurrentTP = Instantiate(teleporter, bottom.transform.position, Quaternion.identity);
                ActiveTP = true;
                AbilityCharges--;
            }
        }
    }
    public void AbilityRecharge()
    {
        AbilityCharges += OnKillRecharge;
    }
}

